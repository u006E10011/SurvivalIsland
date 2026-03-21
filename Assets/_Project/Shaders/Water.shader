Shader "URP/Water"
{
    Properties
    {
        [MainColor] _BaseColor ("Water Color", Color) = (0.2, 0.5, 0.8, 0.8)
        [MainTexture] _BaseMap ("Texture", 2D) = "white" {}
        
        // Wave Settings
        _WaveAmplitude ("Wave Amplitude", Range(0, 2)) = 0.5
        _WaveFrequency ("Wave Frequency", Range(0, 5)) = 0.8
        _WaveSpeed ("Wave Speed", Range(0, 3)) = 1.2
        _WaveCount ("Wave Count", Range(1, 4)) = 3
        
        // Visual Settings
        _Smoothness ("Smoothness", Range(0, 1)) = 0.6
        _Metallic ("Metallic", Range(0, 1)) = 0.1
        _FresnelPower ("Fresnel Power", Range(0, 5)) = 1.5
        _FresnelScale ("Fresnel Scale", Range(0, 1)) = 0.5
        
        // Foam Settings
        _FoamColor ("Foam Color", Color) = (0.9, 0.9, 0.95, 1)
        _FoamIntensity ("Foam Intensity", Range(0, 1)) = 0.3
        _FoamSpeed ("Foam Speed", Range(0, 2)) = 0.5
        
        [Toggle(_NORMAL_MAP)] _UseNormalMap ("Use Normal Map", Float) = 0
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Range(0, 2)) = 1
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back
            
            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile_fog
            
            #pragma shader_feature_local _NORMAL_MAP
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            // ==================== STRUCTS ====================
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                #ifdef _NORMAL_MAP
                float4 tangentOS : TANGENT;
                #endif
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 fogFactorAndVertexLight : TEXCOORD3;
                #ifdef _NORMAL_MAP
                float4 tangentWS : TEXCOORD4;
                float4 bitangentWS : TEXCOORD5;
                #endif
                float3 viewDirWS : TEXCOORD6;
            };
            
            // ==================== PROPERTIES ====================
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            
            #ifdef _NORMAL_MAP
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            #endif
            
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _BaseColor;
                float _WaveAmplitude;
                float _WaveFrequency;
                float _WaveSpeed;
                float _WaveCount;
                float _Smoothness;
                float _Metallic;
                float _FresnelPower;
                float _FresnelScale;
                float4 _FoamColor;
                float _FoamIntensity;
                float _FoamSpeed;
                #ifdef _NORMAL_MAP
                float _NormalStrength;
                #endif
            CBUFFER_END
            
            // ==================== HELPER FUNCTIONS ====================
            
            // Функция волны Герстнера для более реалистичного эффекта
            float GerstnerWave(float3 positionWS, float time, float frequency, float amplitude, float speed)
            {
                float phase = frequency * (positionWS.x + positionWS.z) - time * speed;
                return amplitude * sin(phase);
            }
            
            // Комбинированная волна
            float GetWaveHeight(float3 positionWS, float time)
            {
                float height = 0;
                
                // Первая волна (основная)
                height += GerstnerWave(positionWS, time, _WaveFrequency, _WaveAmplitude, _WaveSpeed);
                
                if (_WaveCount >= 2)
                {
                    // Вторая волна (с меньшей амплитудой и частотой)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 1.8, _WaveAmplitude * 0.6, _WaveSpeed * 1.2);
                }
                
                if (_WaveCount >= 3)
                {
                    // Третья волна (с меньшей амплитудой и частотой)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 2.5, _WaveAmplitude * 0.3, _WaveSpeed * 1.5);
                }
                
                if (_WaveCount >= 4)
                {
                    // Четвертая волна (для большей детализации)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 3.2, _WaveAmplitude * 0.2, _WaveSpeed * 1.8);
                }
                
                return height;
            }
            
            // Вычисление нормали на основе волн
            float3 GetWaveNormal(float3 positionWS, float time, float offset = 0.01)
            {
                float3 normal;
                float height = GetWaveHeight(positionWS, time);
                
                // Вычисляем градиент высоты
                float3 posX = positionWS + float3(offset, 0, 0);
                float3 posZ = positionWS + float3(0, 0, offset);
                
                float heightX = GetWaveHeight(posX, time);
                float heightZ = GetWaveHeight(posZ, time);
                
                float dx = heightX - height;
                float dz = heightZ - height;
                
                normal = normalize(float3(-dx, 1, -dz));
                return normal;
            }
            
            // Эффект Френеля для отражений
            float FresnelEffect(float3 viewDir, float3 normal, float power, float scale)
            {
                float fresnel = pow(1.0 - saturate(dot(viewDir, normal)), power);
                return scale + (1 - scale) * fresnel;
            }
            
            // Пена на гребнях волн
            float FoamEffect(float3 positionWS, float time)
            {
                float waveHeight = GetWaveHeight(positionWS, time);
                float waveSlope = abs(GetWaveHeight(positionWS + float3(0.1, 0, 0.1), time) - waveHeight) / 0.141;
                
                // Пена появляется на высоких точках волн
                float foam = saturate((waveHeight - 0.7) * 2);
                
                // Добавляем эффект на крутых склонах
                foam = max(foam, saturate((waveSlope - 0.5) * 2));
                
                // Анимируем пену
                float timePhase = sin(positionWS.x * 2 + positionWS.z * 2 + time * _FoamSpeed) * 0.5 + 0.5;
                foam = foam * (0.7 + timePhase * 0.3);
                
                return saturate(foam * _FoamIntensity);
            }
            
            // ==================== VERTEX SHADER ====================
            Varyings Vertex(Attributes input)
            {
                Varyings output;
                
                float time = _Time.y;
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                
                // Анимируем вершины
                float waveHeight = GetWaveHeight(positionWS, time);
                positionWS.y += waveHeight;
                
                output.positionWS = positionWS;
                output.positionCS = TransformWorldToHClip(positionWS);
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                
                // Вычисляем нормаль с учетом волн
                float3 normalWS = GetWaveNormal(positionWS, time);
                output.normalWS = normalWS;
                
                // Для Tangent Space Normal Map
                #ifdef _NORMAL_MAP
                float3 tangentWS = TransformObjectToWorldDir(input.tangentOS.xyz);
                float3 bitangentWS = cross(normalWS, tangentWS) * input.tangentOS.w;
                output.tangentWS = float4(tangentWS, 0);
                output.bitangentWS = float4(bitangentWS, 0);
                #endif
                
                // Вектор взгляда
                output.viewDirWS = GetWorldSpaceNormalizeViewDir(positionWS);
                
                // Fog и вершинный свет
                float4 fogFactorAndVertexLight = float4(0, 0, 0, 0);
                fogFactorAndVertexLight.x = ComputeFogFactor(output.positionCS.z);
                
                #ifdef _ADDITIONAL_LIGHTS_VERTEX
                fogFactorAndVertexLight.yzw = VertexLighting(positionWS, normalWS);
                #endif
                
                output.fogFactorAndVertexLight = fogFactorAndVertexLight;
                
                return output;
            }
            
            // ==================== FRAGMENT SHADER ====================
            half4 Fragment(Varyings input) : SV_Target
            {
                float time = _Time.y;
                
                // Основной цвет воды
                float4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
                
                // Получаем нормаль
                float3 normalWS = normalize(input.normalWS);
                
                #ifdef _NORMAL_MAP
                // Если используем Normal Map
                float4 tangentNormal = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, input.uv * 2);
                float3 normalTS = UnpackNormalScale(tangentNormal, _NormalStrength);
                
                float3 tangentWS = normalize(input.tangentWS.xyz);
                float3 bitangentWS = normalize(input.bitangentWS.xyz);
                float3x3 TBN = float3x3(tangentWS, bitangentWS, normalWS);
                normalWS = normalize(mul(normalTS, TBN));
                #endif
                
                // Эффект Френеля
                float fresnel = FresnelEffect(input.viewDirWS, normalWS, _FresnelPower, _FresnelScale);
                
                // Пена
                float foam = FoamEffect(input.positionWS, time);
                
                // Освещение
                InputData inputData;
                inputData.positionWS = input.positionWS;
                inputData.normalizedScreenSpaceUV = 0;
                inputData.shadowMask = 0;
                
                inputData.normalWS = normalWS;
                inputData.viewDirectionWS = input.viewDirWS;
                
                #ifdef _MAIN_LIGHT_SHADOWS
                float4 shadowCoord = TransformWorldToShadowCoord(input.positionWS);
                inputData.shadowCoord = shadowCoord;
                #else
                inputData.shadowCoord = 0;
                #endif
                
                inputData.fogCoord = input.fogFactorAndVertexLight.x;
                inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
                inputData.bakedGI = 0;
                
                // Получаем основной свет
                Light mainLight = GetMainLight(inputData.shadowCoord);
                float3 lightColor = mainLight.color;
                float3 lightDir = mainLight.direction;
                
                // Diffuse освещение
                float NdotL = saturate(dot(normalWS, lightDir));
                float3 diffuse = baseColor.rgb * lightColor * NdotL;
                
                // Specular (блики)
                float3 halfVec = normalize(lightDir + input.viewDirWS);
                float NdotH = saturate(dot(normalWS, halfVec));
                float specularIntensity = pow(NdotH, _Smoothness * 100);
                float3 specular = specularIntensity * lightColor * _Metallic;
                
                // Ambient освещение
                float3 ambient = SampleSH(normalWS) * baseColor.rgb * 0.5;
                
                // Собираем финальный цвет
                float3 finalColor = diffuse + specular + ambient;
                
                // Добавляем отражения через Френель
                finalColor += baseColor.rgb * fresnel * 0.5;
                
                // Добавляем пену
                finalColor = lerp(finalColor, _FoamColor.rgb, foam);
                
                // Прозрачность (пена делает воду менее прозрачной)
                float alpha = baseColor.a * (1 - foam * 0.5);
                
                // Применяем туман
                finalColor = MixFog(finalColor, input.fogFactorAndVertexLight.x);
                
                return half4(finalColor, alpha);
            }
            ENDHLSL
        }
        
        // Shadow Caster Pass
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
            
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull Back
            
            HLSLPROGRAM
            #pragma vertex ShadowVertex
            #pragma fragment ShadowFragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };
            
            CBUFFER_START(UnityPerMaterial)
                float _WaveAmplitude;
                float _WaveFrequency;
                float _WaveSpeed;
                float _WaveCount;
            CBUFFER_END
            
            float GerstnerWave(float3 positionWS, float time, float frequency, float amplitude, float speed)
            {
                float phase = frequency * (positionWS.x + positionWS.z) - time * speed;
                return amplitude * sin(phase);
            }
            
            float GetWaveHeight(float3 positionWS, float time)
            {
                float height = GerstnerWave(positionWS, time, _WaveFrequency, _WaveAmplitude, _WaveSpeed);
                
                if (_WaveCount >= 2)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 1.8, _WaveAmplitude * 0.6, _WaveSpeed * 1.2);
                    
                if (_WaveCount >= 3)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 2.5, _WaveAmplitude * 0.3, _WaveSpeed * 1.5);
                    
                if (_WaveCount >= 4)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 3.2, _WaveAmplitude * 0.2, _WaveSpeed * 1.8);
                    
                return height;
            }
            
            Varyings ShadowVertex(Attributes input)
            {
                Varyings output;
                
                float time = _Time.y;
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                positionWS.y += GetWaveHeight(positionWS, time);
                
                output.positionCS = TransformWorldToHClip(positionWS);
                return output;
            }
            
            half4 ShadowFragment(Varyings input) : SV_TARGET
            {
                return 0;
            }
            ENDHLSL
        }
        
        // Depth Only Pass
        Pass
        {
            Name "DepthOnly"
            Tags { "LightMode" = "DepthOnly" }
            
            ZWrite On
            ColorMask 0
            Cull Back
            
            HLSLPROGRAM
            #pragma vertex DepthVertex
            #pragma fragment DepthFragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };
            
            CBUFFER_START(UnityPerMaterial)
                float _WaveAmplitude;
                float _WaveFrequency;
                float _WaveSpeed;
                float _WaveCount;
            CBUFFER_END
            
            float GerstnerWave(float3 positionWS, float time, float frequency, float amplitude, float speed)
            {
                float phase = frequency * (positionWS.x + positionWS.z) - time * speed;
                return amplitude * sin(phase);
            }
            
            float GetWaveHeight(float3 positionWS, float time)
            {
                float height = GerstnerWave(positionWS, time, _WaveFrequency, _WaveAmplitude, _WaveSpeed);
                
                if (_WaveCount >= 2)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 1.8, _WaveAmplitude * 0.6, _WaveSpeed * 1.2);
                    
                if (_WaveCount >= 3)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 2.5, _WaveAmplitude * 0.3, _WaveSpeed * 1.5);
                    
                if (_WaveCount >= 4)
                    height += GerstnerWave(positionWS, time, _WaveFrequency * 3.2, _WaveAmplitude * 0.2, _WaveSpeed * 1.8);
                    
                return height;
            }
            
            Varyings DepthVertex(Attributes input)
            {
                Varyings output;
                
                float time = _Time.y;
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                positionWS.y += GetWaveHeight(positionWS, time);
                
                output.positionCS = TransformWorldToHClip(positionWS);
                return output;
            }
            
            half4 DepthFragment(Varyings input) : SV_TARGET
            {
                return 0;
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/Lit"
}