/*
 * Created by SharpDevelop.
 * User: adam.moseman
 * Date: 8/13/2017
 * Time: 12:26 AM
 * 
 */
using System;
using System.Collections.Generic;

namespace favoriteslist
{
	class Program
	{
		static List<string> CompareUs = new List<string>();
		
		public static void Main(string[] args)
		{
			string empire = "Empire",
			dwarf="Dwarf",
			vampirecount = "Vampire Counts",
			orcs = "Orcs",
			chaos = "Chaos",
			beastmen = "Beastmen",
			norsca = "Norsca",
			brettonia = "Brettonia",
			woodelf = "Wood Elf",
			darkelf = "Dark Elves",
			highelf = "High Elves",
			lizardman = "Lizardmen",
			skaven = "Skaven",
			tombkings = "Tomb Kings",
			ogres = "Ogres",
			chaosdwarfs = "Chaos Dwarfs",
			daemons = "Daemons";
			AddToList(empire, dwarf, vampirecount, orcs, chaos, beastmen, norsca, brettonia, woodelf, darkelf, highelf, lizardman, skaven, tombkings, ogres, chaosdwarfs, daemons);
			
			bool resolved = false;
			Random rng = new Random();
			List<ComparedThing> CompareUsThings = new List<ComparedThing>();
			for(int i = 0; i<CompareUs.Count; i++)
			{
				CompareUsThings.Add(new ComparedThing(CompareUs[i], rng));
				}
			
			for(int i=0;i<CompareUsThings.Count;i++)
			{
				for(int j=0; j<CompareUsThings.Count; j++)
				{
					if(i!=j)
					{
						
						CompareUsThings[i].AddToToCompare(CompareUsThings[j]);
					}
				}
			}
			
			List<ComparedThing> Compared = new List<ComparedThing>();
			while(CompareUsThings.Count != 0)
			{
				int tocheck = rng.Next(CompareUsThings.Count);
				if(CompareUsThings[tocheck].Compare() == false)
				{
					Compared.Add(CompareUsThings[tocheck]);
					CompareUsThings.RemoveAt(tocheck);
				}
			}
			List<ComparedThing> SortedScoreList = new List<ComparedThing>();
			SortedScoreList.Add(Compared[0]);

			for(int i=1; i<Compared.Count; i++)
			{
				for(int j=0; j<SortedScoreList.Count; j++)
				{
					if(Compared[i].Score > SortedScoreList[j].Score)
					{
						SortedScoreList.Insert(j, Compared[i]);
						break;
					}
				}
				if(!SortedScoreList.Contains(Compared[i]))
				   {
				   	SortedScoreList.Add(Compared[i]);
				   }
			}
			
			for(int i=0; i<SortedScoreList.Count; i++)
			{
				Console.WriteLine(SortedScoreList[i].Name + " | " + SortedScoreList[i].Score);
			}
			
			Console.ReadKey(true);
		}
		
		public static void AddToList(params string[] list) {
			for(int i=0; i< list.Length; i++)
			{
				CompareUs.Add(list[i]);
			}
		}
	}
	
	public class ComparedThing
	{
		public int Score;
		public string Name;
		public List<ComparedThing> ToCompare;
		public Random RNG;
		
		public ComparedThing(string name, Random rng)
		{
			Name = name;
			Score = 0;
			RNG = rng;
			ToCompare= new List<ComparedThing>();
		}
		
		public void AddToToCompare(ComparedThing thing)
		{
			if(!ToCompare.Contains(thing))
			{
				ToCompare.Add(thing);
			}
		}
		
		public bool Compare()
		{
			if(ToCompare.Count==0)
			{
				return false;
			}
			int check = RNG.Next(ToCompare.Count);
			if(ToCompare[check].ToCompare.Contains(this))
			{
				Console.WriteLine("1:"+Name + " or 2:" + ToCompare[check].Name);
				
				while(true)
				{
					char returnvalue = Console.ReadKey(true).KeyChar;
					
					if(returnvalue == '1')
					{
						Score++;
						break;
					}
					if(returnvalue == '2')
					{
						ToCompare[check].Score++;
						break;
					}
				}
				ToCompare[check].ToCompare.Remove(this);
			}
			ToCompare.RemoveAt(check);
			if(ToCompare.Count==0)
			{
				return false;
			}
			return true;
		}
		
	}
}
