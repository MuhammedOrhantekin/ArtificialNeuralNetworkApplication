using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Proje1_YapaySinirAglari
{
    internal class program
    {
        static void Main()
        {
            double[][] trainingData = {
            new double[] {7.6, 8, 6.6, 8.4, 8.8, 7.2, 8.1, 9.5, 7.3, 8.9, 7.5, 7.6, 7.9, 8, 7.2, 8.8, 7.6, 7.5, 9, 7.7, 8.1},
            new double[] {11, 10, 8, 10, 12, 10, 11, 9, 9, 11, 11, 9, 10, 10, 9, 10, 11, 10, 10, 9, 11},
            new double[] {77, 70, 55, 78, 95, 67, 80, 87, 60, 88, 72, 58, 70, 76, 58, 81, 74, 67, 82, 62, 82},
        };

            int workHoursFactor = 10;
            int attendanceFactor = 15;
            int examResultFactor = 100;

            // Verileri normalize et
            for (int i = 0; i < trainingData[0].Length; i++)
            {
                trainingData[0][i] /= workHoursFactor;
                trainingData[1][i] /= attendanceFactor;
                trainingData[2][i] /= examResultFactor;
            }

            // Normalize edilmiş verileri ayrı array'lere yerleştir
            double[][] inputs = new double[trainingData[0].Length][];
            double[] outputs = new double[trainingData[0].Length];

            for (int i = 0; i < trainingData[0].Length; i++)
            {
                inputs[i] = new double[] { trainingData[0][i], trainingData[1][i] };
                outputs[i] = trainingData[2][i];
            }


            // Nöron oluşturma
            Neuron neuron = new Neuron(inputs[0].Length);

            // Eğitim
            double learningRate = 0.05;
            int epochs = 10;
            double totalMSE = 0;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    double output = neuron.ComputeOutput(inputs[i]);
                    neuron.UpdateWeights(learningRate, outputs[i], output, inputs[i]);
                }

                if ((epoch + 1) % 10 == 0) // Her 10 epoch'ta bir
                {
                    double epochMSE = MeanSquareError(neuron, inputs, outputs);
                    totalMSE += epochMSE;
                    
                }


            }

            // Tahminleme
            Console.WriteLine("Tahminlenen Değerler:");
            for (int i = 0; i < inputs.Length; i++)
            {
                double predicted = neuron.ComputeOutput(inputs[i]);
                Console.WriteLine($"Girdiler : {inputs[i][0] * (workHoursFactor)} ,   {inputs[i][1] * attendanceFactor}         Sonuç: {outputs[i] * examResultFactor}          Tahmin: {predicted * examResultFactor}");
            
            }

            Console.WriteLine();

            // modelin görmediği veriden sınav sonucu tahmin hesaplama
            double[][] newInputs =
            {
                    new double[] { 5.5 , 6.5 , 6.9 , 9.9, 9.1 },
                    new double[] { 12  , 11  , 14  , 10 , 9  },

                };

            // Verileri normalize et
            for (int i = 0; i < newInputs[0].Length; i++)
            {
                newInputs[0][i] /= workHoursFactor;
                newInputs[1][i] /= attendanceFactor;
            }


            double[][] inputs2 = new double[newInputs[0].Length][];
            for (int i = 0; i < newInputs[0].Length; i++)
            {
                inputs2[i] = new double[] { newInputs[0][i], newInputs[1][i] };
            }

            Console.WriteLine("Modelin Görmediği Veriden Sınav Sonucu Tahminleme: ");
            for (int i = 0; i < inputs2.Length; i++)
            {
                double predicted = neuron.ComputeOutput(inputs2[i]);
                Console.WriteLine($"Girdi: {inputs2[i][0] * workHoursFactor}  ,   {inputs2[i][1] * attendanceFactor}        Tahmin: {predicted * examResultFactor}");
            }
            Console.WriteLine();

            double averageMSE = totalMSE / (epochs / 10);
            Console.WriteLine($"Toplam Mean Square Error {epochs} iken : {averageMSE}");

            Console.ReadLine();

        }
        // MSE değerini hesaplayan Meto
        static double MeanSquareError(Neuron neuron, double[][] inputs, double[] outputs)
        {
            double sumSquaredError = 0.0;

            for (int i = 0; i < inputs.Length; i++)
            {
                double predicted = neuron.ComputeOutput(inputs[i]);
                double error = outputs[i] - predicted;
                sumSquaredError += Math.Pow(error, 2);
            }

            return (sumSquaredError / inputs.Length) * 100;
        }

    }
}