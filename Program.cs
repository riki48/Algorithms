using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApp1
{
	

	public class BinaryTree
	{
		public BinaryTree left;
		public BinaryTree right;
		public BinaryTree parent;
		public int data;

		public void Show()
		{
			Queue<BinaryTree> queue = new Queue<BinaryTree>();
			queue.Enqueue(this);
			while (queue.Count != 0)
			{
				BinaryTree temp = queue.Dequeue();
				Console.WriteLine(temp.data);
				queue.Enqueue(temp.right);
				queue.Enqueue(temp.left);
			}
		}

		public void Add(int n)
		{
			if (data == 0)
			{
				data = n;
				return;
			}
			if (n > data)
			{
				if (right == null)
				{
					right = new BinaryTree() { data = n };
					right.parent = this;
				}
				else right.Add(n);
			}
			else
			{
				if (left == null)
				{
					left = new BinaryTree() { data = n };
					left.parent = this;
				}
				else left.Add(n);
			}
		}
		public BinaryTree Find(int n)
		{
			if (data == n)
			{
				return this;
			}
			if (n > data)
			{

				if (right == null)
				{
					Console.WriteLine("Not Found");
					return null;
				}
				else return right.Find(n);
			}
			else
			{

				if (left == null)
				{
					Console.WriteLine("Not Found");
					return null;
				}
				else return left.Find(n);
			}
		}
		public int HorizontalSearch()
		{
			int min = int.MaxValue;
			Queue<BinaryTree> queue = new Queue<BinaryTree>();
			var tmp = this;
			queue.Enqueue(tmp);
			while (queue.Count != 0)
			{
				var elem = queue.Dequeue();
				if (elem.left == null && elem.right == null && data < min)
				{
					min = elem.data;
					continue;
				}
				if (elem.left != null) queue?.Enqueue(elem.left);
				if (elem.right != null) queue?.Enqueue(elem.right);
			}
			return min;
		}

		public void Delete(int n)
		{
			BinaryTree tree = Find(n);

			if (tree.left == null && tree.right == null)
				if (tree.parent.right == tree)
					tree.parent.right = null;
				else if (tree.parent.left == tree)
					tree.parent.left = null;

			if (tree.right != null && tree.left == null)
				if (tree.parent.right == tree)
				{
					tree.parent.right = tree.right;
					tree.right.parent = tree.parent;
				}
				else
				{
					tree.parent.left = tree.right;
					tree.right.parent = tree.parent;
				}
			if (tree.right == null && tree.left != null)
				if (tree.parent.right == tree)
				{
					tree.parent.right = tree.left;
					tree.left.parent = tree.parent;
				}
				else
				{
					tree.parent.left = tree.left;
					tree.left.parent = tree.parent;
				}
			if (tree.right != null && tree.left != null)
			{
				BinaryTree temp = tree.right;
				while (temp.left != null)
					temp = temp.left;	 
				int tData = temp.data;
				Delete(temp.data);
				tree.data = tData;
			}
		}
		
	}
	
	public class HashTable
	{
		public class HashItem
		{
			public string Name;
			public int Price;
			public HashItem nextItem;
			public HashItem(string name,int price)
			{
				Name = name;
				Price = price;
			}
		}
		HashItem[] PriceArr;
		private void ArrRenew()
		{
			HashTable table = new HashTable(PriceArr.Length * 2);
			Queue<HashItem> queue = new Queue<HashItem>();
			for (int i = 0; i < PriceArr.Length; i++)
			{
				HashItem temp = PriceArr[0];
				while (temp != null)
				{
					table.Add(temp.Name,temp.Price);
					temp = temp.nextItem;
				}
			}
			PriceArr = table.PriceArr;
		}
		
		public int this[string index]
		{
			get { return 1; }
			set { Add(index,value); }
		}
		public void Add(string s,int price)
		{
			int n = HashFunc(s);
			if (PriceArr[n] == null) PriceArr[n] = new HashItem(s, price);
			else
			{
				HashItem temp = PriceArr[n];
				do
				{
					if (temp.Name == s)
					{
						temp.Price = price;
						return;
					}
					temp = temp.nextItem;
				}
				while (temp?.nextItem != null);
					temp.nextItem = new HashItem(s,price);
			}
		}


		public HashTable(int n)
		{
			PriceArr = new HashItem[n];
		}
		public int HashFunc(string s)
		{
			if (s[0] == 'a') return 0;
			if (s[0] == 'b') return 1;
			return 0;

			
		}
	}
	public class StationEventArgs
	{
		public string name;
		public double temperature;
		public StationEventArgs(string Name, double Temperature)
		{
			name = Name; temperature = Temperature;
		}
	}
	class AtomicStation
	{
		
		public delegate void StationHandler(object sender, StationEventArgs e);
		public event StationHandler Alarm;
		
		string Status = "Online";
		static int count = 1;
		string Name = "Station Number " + count;
		public double Temperature;
		
		public void TempUp(int val)
		{

			if (Temperature + val >= 100)
			{
				Temperature += val;
				Alarm(this, new StationEventArgs(Name, Temperature));
				return;
			}
			Console.WriteLine("Current temperature is " + (int)(Temperature+val));
		}
		
		public AtomicStation(double Temperature)
		{
			Alarm += Shutdown;
			Alarm += Report;
			this.Temperature = Temperature;
			count++;
		}
		public void Shutdown(object sender, StationEventArgs e)
		{
			Console.WriteLine("Reactor Overheat! - " + e.temperature +" Celsium");
			Console.WriteLine("Emergency shutdown...");
			Status = "Offline";
		}
		public void Report(object sender, StationEventArgs e)
		{
			var s =( AtomicStation)sender;
			Console.WriteLine($"Reporting: {s.Name} \nStatus: {s.Status} \nCore Temperature: {s.Temperature} \n");
		}


	}
	public static class Program
	{
		static class QuickSortClass
		{
			static string FiveOne(string s)
			{
			string temp = "";

				for (int i = 0; i < s.Length; i++)
				{
					temp = s[i] + temp;
			    }
			return temp.Trim();
		} //5
		public static string StrVerse(string s)
		{
			string result = "";
			string temp = "";
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] != ' ')
					temp += s[i];
				if (s[i] == ' ')
				{
					result = temp + " " + result;
					temp = "";
				}
			}
			result = temp + " " + result;
			return result;

		}  //8
		public static void QuickSort(int[] arr) //12
		{
			QuickSort(arr, 0, arr.Length - 1);		
		}
		private static void QuickSort(int[] arr,int leftEnd,int rightEnd)
		{
			if (leftEnd >= rightEnd) return;
			int PivotLocation = ChoosePivotLocation(arr, leftEnd, rightEnd);
			PivotLocation = SortPivot(arr, leftEnd, PivotLocation, rightEnd);
			QuickSort(arr, leftEnd, PivotLocation - 1);
			QuickSort(arr, PivotLocation + 1, rightEnd);
		}

		private static int SortPivot(int[] arr, int leftEnd, int pivotLocation, int rightEnd)
		{
			var pivot = arr[pivotLocation];
			Swap(arr, pivotLocation, rightEnd);
			int leftIndex = leftEnd;
			int rightIndex = rightEnd - 1;
			while (leftIndex<=rightIndex)	
			{
				if (arr[leftIndex] <= pivot)
				{
					leftIndex++;
					continue;
				}
				if(arr[rightIndex] >= pivot)
				{
					rightIndex--;
					continue;
				}
				Swap(arr, leftIndex, rightIndex);  
			}
			Swap(arr, rightEnd,leftIndex );
			return leftIndex;
		}

		private static int ChoosePivotLocation(int[] arr, int leftEnd, int rightEnd)
		{
			return leftEnd + (rightEnd - leftEnd) / 2;
		}

		static int [] Swap(int [] arr)
		{
			int SwapIndex = -1;
			int CounterIndex = 0;
			for (int i = 0; i < arr.Length - 1; i++)
			{
				if (arr[arr.Length - 1] > arr[CounterIndex])
				{
					SwapIndex++;
					int temp = arr[CounterIndex];
					arr[CounterIndex] = arr[SwapIndex];
					arr[SwapIndex] = temp;
				}
				CounterIndex++;
			}
			int temp1 = arr[SwapIndex + 1];
			arr[SwapIndex + 1] = arr[arr.Length - 1];
			arr[arr.Length - 1] = temp1;
			return arr;
		}
		static void Swap(int[] arr,int i,int j)
		{
			int temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		} }

		
		
		public static void Main(string[] args)
		{

			//tree.Add(3);
			//tree.Add(2);
			//tree.Add(1);
			//tree.Add(9);
			//tree.Add(10);
			//tree.Add(7);
			//tree.Add(6);
			//tree.Add(8);
			//tree.Delete(3);

			//   Console.ForegroundColor=ConsoleColor.DarkMagenta;
			//HastTable hs = new HastTable();

			//var s1 = new AtomicStation(50);
			//var s2 = new AtomicStation(60);
			//var s3 = new AtomicStation(99);
			//s3.TempUp(101);
			//s1.TempUp(101);

			//Node node = new Node() { Data = 1 };
			//node.Add(4);
			//node.Add(5);

			//Node secondNode = new Node() { Data = 1 };
			//secondNode.Add(3);
			//secondNode.Add(4);
			//secondNode.Add(10);

			//node.Display(node.ReverseRecursive(node));
			//FiveOne(@"C:\Users\worker\Desktop");

			////string s = "Hello World i love apples"; Console.WriteLine(StrVerse(s));
			//int[] arr = new int[10];

			//var rnd = new Random();
			//for (int i = 0; i < 10; i++)
			//{
			//	arr[i] = rnd.Next(0, 10);
			//}
			//foreach (var item in arr)
			//{
			//	Console.Write(item + " ");
			//}
			//Console.WriteLine();
			//QuickSort(arr);
			//foreach (var item in arr)
			//{
			//	Console.Write(item +" ");
			//}
			HashTable table = new HashTable(2);
			table.Add("apple", 55);
			table.Add("apple", 56);
			table.Add("appe", 52);
			table.Add("apple", 56);
			table.Add("ale", 56);


		}
	}

	public class Node
	{
		
		public int Data;
		public Node Nextnode;
		public Node(int n)
		{
			Data = n;
		}
		public int Length()
		{
			int count=1;
			
			Node temp = this;
			while (temp?.Nextnode != null)
			{
				count++;		
				temp = temp.Nextnode;
			}
			return count;
		}
		public Node ReverseRecursive(Node root)  // 7.2 //
		{
			Node temp = root;
			if (root.Nextnode == null)
				return root;
			else
				root = ReverseRecursive(root.Nextnode);
			temp.Nextnode = null;
			Node tail = root.Nextnode;
			if (tail == null)
				root.Nextnode = temp;
			else
				while (tail != null)
				{
					if (tail.Nextnode == null)
					{
						tail.Nextnode = temp;
						break;
					}
					else
						tail = tail.Nextnode;
				}
			return root;
		}
		public void Add(int n)
		{
			Node temp = this;
			
			while (temp.Nextnode != null)
				temp = temp.Nextnode;
			temp.Nextnode = new Node() { Data = n };
		}
		public void Display(Node node)
		{
			Node temp = node;
			while (temp != null)
			{
				Console.WriteLine(temp.Data);
				temp = temp?.Nextnode;
			}
		}
		public Node SearchIndex(int index)
		{
			int count = 0;
			Node temp = this;
			for (int i = 0; i <= index; i++)
			{
				if (temp == null)
				{
					Console.WriteLine("Not found");
					return null;
				}
				if(i==index)
				{
					//Console.WriteLine("Value is " + temp.Data);
					return temp;
				}
			 temp = temp.Nextnode;
				
			}
			Console.WriteLine("BUGBUG13213213331");
			return null;
		
		}
		public void SearchVal(int value)
		{
			Node current = this;
			for (int i = 0; current.Nextnode != null; i++)
			{
				if (current.Data != value)
				{
					current= current.Nextnode;
					continue;
				}
				else Console.WriteLine(i);
				return;
			}
			Console.WriteLine("Элемент со значением не найдет");
			
		}
		public Node() { }
		//public Node Substract (Node SecondNode)
		//{
			
		//}
		public Node Unite(Node SecondNode)
		{
			Node FirstNode = this;

			Node NewNode = new Node() { Data = 0 };
			int fi = 0;
			int si = 0;
			int firstLength = FirstNode.Length();
			int secondLength = SecondNode.Length();
			int length = firstLength > secondLength ? firstLength : secondLength;
			int[] Arr = new int[firstLength+secondLength];
			
			for (int i = 0,j=0 ; i < length; i++)
			{
				if (fi<firstLength)
				{
					int fData = FirstNode.SearchIndex(fi).Data;
					if (!Arr.Contains(fData))
					{
						NewNode.Add(fData);
						Arr[j] = fData;
						j++;
					}
					fi++;
				}
				if (si < secondLength)
				{
					int sData = SecondNode.SearchIndex(si).Data;
					if (!Arr.Contains(sData))
					{
						NewNode.Add(sData);
						Arr[j] = sData;
						j++;
					}
					si++;	 
				}
			}
			return NewNode;
		}
		public Node Cross(Node SecondNode)
		{
			Node FirstNode = this;
			
			Node NewNode = new Node() { Data=0};
			int fi= 0;
			int si = 0;
			int firstLength = FirstNode.Length();
			int secondLength = SecondNode.Length();
			int length = firstLength > secondLength ? firstLength : secondLength;
			int[] Arr = new int[length];
			for (int i = 0; i < length ; i++)
			{
				int fData = FirstNode.SearchIndex(fi).Data;
				int sData = SecondNode.SearchIndex(si).Data;
				if (fData == sData)
				{
					if (!Arr.Contains(fData))
					{
						NewNode.Add(fData);
						fi++;
						si++;
						Arr[i] = fData;
						continue;
					}
				}
				if (sData < fData) si++;
				if (fData <sData) fi++;
			}
			return NewNode;
		}
		
	}
}
