using Shared_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace Prototype
{
    class NexusWriter
    {
        private NexusFile nexusOb;
        public NexusWriter(NexusFile n)
        {
            nexusOb = n;
        }

        public async void WriteToFile()
        {

            string path = @"c:\temp\MyTest.txt";

            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter file = File.CreateText(path))
                {
                    System.IO.TextWriter
                    file.WriteLine("#NEXUS");
                    file.WriteLine("BEGIN TAXA;");
                    file.WriteLine("Dimensions NTax=" + nexusOb.T.taxa.Count);
                    file.Write("TaxLabels ");

                    foreach (string taxon in nexusOb.T.taxa)
                    {
                        file.Write(taxon + " ");
                    }
                    file.WriteLine();

                    file.WriteLine("END;");

                    file.WriteLine("BEGIN CHARACTERS;");
                    file.WriteLine(@"DIMENSIONS NChar=""");
                    file.WriteLine("MATRIX");
                    foreach (Sequence s in App.f.C.sequences)
                    {
                        file.WriteLine(s.name + " " + s.characters);
                    }
                    file.WriteLine(";");
                    file.WriteLine("END;");
                }
            }
            


            
        }
    }
}
