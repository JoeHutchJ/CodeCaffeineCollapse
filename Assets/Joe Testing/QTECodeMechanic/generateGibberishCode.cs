using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;



public static class generateGibberishCode {

     static string[] keywords = { "if", "else", "while", "for" };
       static string[] types = { "int", "char", "Vector2", "String", "Boolean", "float"};
       static string[] comparison = { ">", ">=", "<=", "<", "==", "!="};
       static string[] operators = { "*", "/", "+", "-", "%"};
      static  string[] andor = { "&&", "||"};

    static int totalLines = 0;

    static int numLines = 0;
    public static  string GenerateRandomCode(int _numLines)
    {
        StringBuilder code = new StringBuilder();
        numLines = _numLines;
        while (!lineLimit())
        {
            code.Append(GenerateGenericNest(false, 0));

        }
        
        return code.ToString();
    }

    


    public static string ParamBuilder() {
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

    public static string randomType() {

        return types[UnityEngine.Random.Range(0, types.Length)];

    }


    public static string GenerateGenericLine(int numTabs) {
        StringBuilder code = new StringBuilder();

        string param1 = ParamBuilder();

        for (int j = 0; j < numTabs; j++)
            {
                code.Append("\t");
            }
                if (UnityEngine.Random.Range(0,10) >= 7) {
                    code.Append(param1 + "()");

                } else {

                code.AppendFormat("{0} {1} = ", randomType(), param1);
                int numParams = UnityEngine.Random.Range(1,3); 
                for (int k = 0; k < numParams; k++) {
                    if (UnityEngine.Random.Range(0,10) >= 7 && k != numParams - 1) {
                        code.AppendFormat("{0}() {1} ", ParamBuilder(),operators[UnityEngine.Random.Range(0, operators.Length)]);
                    } else if (k != numParams - 1) {
                        code.AppendFormat("{0} {1} ", ParamBuilder(),operators[UnityEngine.Random.Range(0, operators.Length)]);

                    } else {
                        code.AppendFormat("{0}", ParamBuilder());
                    }
                }
                
                }
                code.Append(";");
                totalLines++;

        return code.ToString();
    }

    public static string GenerateGenericNest(Boolean endNest, int numTabs) {
        StringBuilder code = new StringBuilder();

        for (int j = 0; j < numTabs; j++)
            {
                code.Append("\t");
            }

        string keyword = keywords[UnityEngine.Random.Range(0, keywords.Length)];
        if (keyword != "else") {
            code.AppendFormat("{0}(", keyword);

        } else {
            code.AppendFormat("{0}", keyword);
        }


            
            
            if (keyword == "if" || keyword == "while") {
                   int numParams = UnityEngine.Random.Range(1,3); 
                   for (int k = 0; k < numParams; k++)
            {
                string param1 = ParamBuilder();
                string param2 = ParamBuilder();
                code.AppendFormat("{0} {1} {2}", param1, comparison[UnityEngine.Random.Range(0, comparison.Length)], param2);
                if (k != numParams -1) {
                    code.Append(" " + andor[UnityEngine.Random.Range(0, andor.Length)] + " ");
                } 
                
            }
                code.Append(")");
            } else if (keyword == "for") {
                string param1 = ParamBuilder();
                code.AppendFormat("int i = 0; i < {0}; i++", param1);
                code.Append(")");
            } else if (keyword == "else") {
            } 
            code.Append(" {");
            int numsubLines = UnityEngine.Random.Range(1,5);
            code.AppendLine("");
            totalLines++;


            for (int i = 0; i< numsubLines; i++) {
                if (!lineLimit()) {
                if (UnityEngine.Random.Range(1,10) >= 9 && !endNest) {
                    code.Append(GenerateGenericNest(true, numTabs+1));
                } else {
                code.AppendLine(GenerateGenericLine(numTabs+1));

                }
                }
            }
            for (int j = 0; j < numTabs; j++)
            {
                code.Append("\t");
            }
            code.AppendLine("}");
            totalLines++;

            

        return code.ToString();
    }


    static bool lineLimit() {
        return totalLines > numLines;
    }

    

}
