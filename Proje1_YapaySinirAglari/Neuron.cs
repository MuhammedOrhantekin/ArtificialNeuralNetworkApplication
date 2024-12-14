using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje1_YapaySinirAglari
{
    internal class Neuron
    {
        private double[] weights;

        public Neuron(int inputCount)
        {
            // Ağırlıkları [0, 1] arasında rastgele değerlerle başlat
            Random rand = new Random();
            weights = new double[inputCount];
            for (int i = 0; i < inputCount; i++)
            {
                weights[i] = rand.NextDouble();
            }
        }

        public double ComputeOutput(double[] inputs)
        {
            if (inputs.Length != weights.Length)
            {
                throw new ArgumentException("Girdi sayısı ağırlık sayısıyla eşleşmiyor.");
            }

            double sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }
            return sum;
        }

        public double[] GetWeights()
        {
            return weights;
        }

        public void UpdateWeights(double learningRate, double target, double output, double[] inputs)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] + learningRate * (target - output) * inputs[i];
            }
        }
    }
}
