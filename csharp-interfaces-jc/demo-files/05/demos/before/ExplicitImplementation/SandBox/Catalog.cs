using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    public class Catalog : ISaveable, IDbSaver
	{
		public void Save()  // Usable on Catalog and ISaveable
		{
			Console.WriteLine("Saved from ISaveable interface"); 
		}
		string IDbSaver.Save() // Usable on IDbSaver ( Explicit )
		{
			return ("Saved from IDbSaver interface");
		}
	}
}