using System;
using System.Collections;
using System.IO;

namespace ResumeGenerator
{
	public class ResumeGenerator
	{

		public ResumeGenerator(string[] args)
		{
			generators = new ArrayList();
			HandleArgs(args);
            combiner = new Combiner();
            CreateGenerators();
			File.WriteAllText(outputName, Combine());
		}

		private ArrayList generators;
		private Combiner combiner;
		private string dirName, outputName;
		private string version = "Html Resume Generator v0.1";

		private void HandleArgs(string[] args)
		{
			int current = 0;
			if(args.Length == 0) { WriteUsage(); }

			// [OPTIONS]
			if(args[current].StartsWith("-")) {
				if(args[0].Equals("-h") || args[0].Equals("--help")) {
					WriteUsage();
				}
				if(args[0].Equals("-v") || args[0].Equals("--version")) {
					Console.WriteLine(version);
					Environment.Exit(0);
                }else {
                    Console.WriteLine("invalid argument: " + args[0]);
                }
			}

			dirName = args[current];
			++current;
			if(current == args.Length){
				outputName = dirName + "resume.html";
			} else {
				outputName = args[current];
			}
		}

		private void WriteUsage()
		{
			Console.Write("usage: [options] target-directory [output-filename] \n" +
			"\n OPTIONS \n" +
			"    -h --help    displays usage information \n" +
			"    -v --version displays version information \n");
			Environment.Exit(0);
		}

		private void CreateGenerators()
		{
			// add cover letter
			generators.Add(new StaticHtmlGenerator(dirName, "/cover.html", ReplacementTags.COVER_TAG));

			// add about section
			generators.Add(new StaticHtmlGenerator(dirName, "/about.html", ReplacementTags.ABOUT_TAG));

			// add education section
			generators.Add(new StaticHtmlGenerator(dirName, "/education.html", ReplacementTags.EDUCATION_TAG));

			// add links section
			// add contact section
			// add greeting section
			// add skills section
			// add xp section
		}

		private string Combine()
		{
			foreach (HtmlGenerator gen in generators) 
			{
				combiner.AddSection(gen.replacementTag, gen.GetHtml());
			}
			if (!combiner.CreateFinishedDoc(dirName)) 
			{
				throw new Exception("Missing or corrupted resource");
			}
			return combiner.GetHtml();
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
                ResumeGenerator fhg  = new ResumeGenerator(args);
		}
	}
}
