using System;
using System.Linq;

class Neuron2Lesson
{

	struct OptimizerNeuron // 1. Свойства позволяют взаимодействовать с переменными 
						   // 2. модификаторы доступа разграничивают
						   // 3.Структура позволила сильно ускорить выполненние процесса
	{
		internal double a { get; set; }
		internal double b { get; set; }
		internal int c { get; set; }
		internal double[] w { get; set; }

		internal int[][] X { get; set; }
		internal int[] Y { get; set; }
		internal int[][] Test { get; set; }

		// public double[] w = { 0, 0, 0, 0 };


		internal OptimizerNeuron(double a, double b, int c, double[] w, int[][] X, int[] Y, int[][] Test) // Конструктор с  параметрами
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.w = w;
			this.X = X;
			this.Y = Y;
			this.Test = Test;

		}

		// Заменяем стандартный конструктор на этот
		public OptimizerNeuron()
		{
			this.a = 0.02;
			this.b = -0.4;
			this.c = 0;
			this.w = new double[4] { 0, 0, 0, 0 };
			this.X = new int[][] {
				new int[] {0, 0, 0, 0},
				new int[] {0, 0, 0, 1},
				new int[] { 1, 1, 1, 0 },
				new int[] { 1, 1, 1, 0 },
				new int[] { 1, 1, 1, 1 }

			};
			this.Y = new int[5] { 0, 1, 1, 0, 1 };
			this.Test = new int[][] {
				new int [] {0, 0, 0, 0},
				new int [] {0, 0, 0, 1},
				new int [] {0, 1, 0, 1},
				new int [] {0, 1, 1, 0},
				new int [] {1, 1, 1, 0},
				new int [] {1, 1, 1, 1}
			};

		}
		public OptimizerNeuron(int[][] X, int[] Y) : this()
		{
			while (learn(X, Y))
			{
				if (c++ > 10000) break;
			}
		}

		public double calculate(int[] x)
		{
			double s = b;
			for (int i = 0; i < w.Length; i++) s += w[i] * x[i];
			return (s > 0) ? 1 : 0;
		}
		bool learn(int[][] X, int[] Y)
		{
			double[] w_ = new double[w.Length];

			Array.Copy(w, w_, w.Length);

			int i = 0;
			foreach (int[] x in X)
			{
				int y = Y[i++];
				for (int j = 0; j < x.Length; j++)
				{
					w[j] += a * (y - calculate(x)) * x[j];
				}
			}
			return !Enumerable.SequenceEqual(w_, w);
		}

		internal int Start()
		{
			OptimizerNeuron neuron = new OptimizerNeuron(X, Y);
			Console.WriteLine("[{0}] {1}",
				string.Join(", ", neuron.w),
				neuron.c
				);

			foreach (int[] test in Test)
			{
				Console.WriteLine("[{0}] {1} {2}",
					string.Join(", ", test),
					test[3],
					neuron.calculate(test)
				);
			}
			return 0;
		}

		public override string ToString()
		{
			return $"({a},{b},{c},{w},{X},{Y},{Test})";
		}
		public static int Main()
			{
				OptimizerNeuron start = new OptimizerNeuron();
				start.Start();

				return 0;
			}
		
	}
}	
