using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;



public class generateGibberishCode : MonoBehaviour
{

     string[] keywords = { "if", "else", "while", "for" };
        string[] types = { "int", "char", "Vector2", "String", "Boolean", "float"};
        string[] comparison = { ">", ">=", "<=", "<", "==", "!="};
        string[] operators = { "*", "/", "+", "-", "%"};
        string[] andor = { "&&", "||"};

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GenerateRandomCode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  string GenerateRandomCode()
    {
        StringBuilder code = new StringBuilder();
        int numLines = UnityEngine.Random.Range(5, 11);


        
        for (int i = 0; i < numLines; i++)
        {
            int numTabs = UnityEngine.Random.Range(1,5);
            /*for (int j = 0; j < numTabs; j++)
            {
                code.Append("\t");
            }*/
            
            
            

            
            
            code.AppendLine("");
        }
        
        return code.ToString();
    }

    


    public string ParamBuilder() {
        string[] technicalWords = {
    "numIterations", "maxThreads", "bufferSize", "dataLength", "outputFormat",
    "encryptionKey", "compressionLevel", "inputStream", "logFilePath", "timeoutSeconds",
    "precision", "batchSize", "numHiddenLayers", "learningRate", "momentum",
    "dropoutRate", "convolutionSize", "poolingSize", "numFilters", "stride",
    "kernelSize", "biasEnabled", "activationFunction", "lossFunction", "optimizer",
    "inputShape", "outputShape", "hiddenLayerSizes", "weightDecay", "decayRate",
    "initialLearningRate", "decaySteps", "numClasses", "inputChannels", "outputChannels",
    "numNeurons", "filterShape", "numKernels", "maxPooling", "minPooling",
    "numEpochs", "batchNormalization", "weightInitialization", "momentumDecay", "earlyStopping",
    "warmStart", "kernelInitializer", "biasInitializer", "recurrentActivation", "returnSequences"
};

    

    return technicalWords[UnityEngine.Random.Range(0, technicalWords.Length)];

    }

    public string randomType() {

        return types[UnityEngine.Random.Range(0, types.Length)];

    }


    public string GenerateGenericLine() {
        StringBuilder code = new StringBuilder();

        string param1 = ParamBuilder();
                if (UnityEngine.Random.Range(0,10) >= 7) {
                    code.Append(param1 + "()");

                } else {

                code.AppendFormat("{0} {1} = ", randomType(), param1);
                int numParams = UnityEngine.Random.Range(1,3); 
                for (int k = 0; k < numParams; k++) {
                    if (UnityEngine.Random.Range(0,10) >= 7) {
                        code.AppendFormat("{0}() {1} ", ParamBuilder(),operators[UnityEngine.Random.Range(0, operators.Length)]);
                    } else if (k != numParams - 1) {
                        code.AppendFormat("{0} {1} ", ParamBuilder(),operators[UnityEngine.Random.Range(0, operators.Length)]);

                    } else {
                        code.AppendFormat("{0}", ParamBuilder());
                    }
                }
                
                }
                code.Append(";");
        code.AppendLine("");

        return code.ToString();
    }

    public string GenerateGenericNest(Boolean endNest) {
        StringBuilder code = new StringBuilder();

        string keyword = keywords[UnityEngine.Random.Range(0, keywords.Length)];
            code.AppendFormat("{0}(", keyword);

            
            
            if (keyword == "if" || keyword == "while") {
                   int numParams = UnityEngine.Random.Range(1,3); 
                   for (int k = 0; k < numParams; k++)
            {
                string param1 = ParamBuilder();
                string param2 = ParamBuilder();
                code.AppendFormat("{0} {1} {2}", param1, comparison[UnityEngine.Random.Range(0, comparison.Length)], param2);
                if (k != numParams -1) {
                    code.Append(" " + andor[UnityEngine.Random.Range(0, andor.Length)] + " ");
                } else {
                    code.Append(")");
                }
                
                
            }
                code.Append(")");
            } else if (keyword == "for") {
                string param1 = ParamBuilder();
                code.AppendFormat("int i = 0; i < {0}; i++", param1);
                code.Append(")");
            } else if (keyword == "else") {
                code.Append(")");
            } 

            int numsubLines = UnityEngine.Random.Range(1,5);
            for (int i = 0; i< numsubLines; i++) {
                if (UnityEngine.Random.Range(1,10) >= 9 && !endNest) {
                    code.Append(GenerateGenericNest(true));
                } else {
                code.Append(GenerateGenericLine());

                }
            }

            

        return code.ToString();
    }

    

}
