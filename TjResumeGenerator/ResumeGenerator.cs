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

        ArrayList generators;
        Combiner combiner;
        string dirName, outputName;
        string version = "Html Resume Generator v0.1";

        void HandleArgs(string[] args)
        {
            int current = 0;
            if (args.Length == 0) { WriteUsage(); }

            // [OPTIONS]
            if (args[current].StartsWith("-", StringComparison.Ordinal))
            {
                if (args[0].Equals("-h") || args[0].Equals("--help"))
                {
                    WriteUsage();
                }
                if (args[0].Equals("-v") || args[0].Equals("--version"))
                {
                    Console.WriteLine(version);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("invalid argument: " + args[0]);
                }
            }

            dirName = args[current];
            ++current;
            if (current == args.Length)
            {
                outputName = DocumentNames.GetPath(dirName, "resume.html");
            }
            else
            {
                outputName = args[current];
            }
        }

        void WriteUsage()
        {
            Console.Write("usage: [options] target-directory [output-filename] \n" +
            "\n OPTIONS \n" +
            "    -h --help    displays usage information \n" +
            "    -v --version displays version information \n");
            Environment.Exit(0);
        }

        void CreateGenerators()
        {
            try
            {
                // add script
                generators.Add(new StaticHtmlGenerator(dirName, DocumentNames.JS_DOC,
                                                       ReplacementTags.SCRIPT_TAG));

                // add style
                generators.Add((new StaticHtmlGenerator(dirName, DocumentNames.STYLE_DOC,
                                                        ReplacementTags.STYLE_TAG)));

                // add name
                generators.Add(new StaticHtmlGenerator(dirName, DocumentNames.NAME_DOC,
                                                       ReplacementTags.NAME_TAG));

                // add cover letter
                generators.Add(new StaticHtmlGenerator(dirName, DocumentNames.COVER_DOC,
                                                       ReplacementTags.COVER_TAG));

                // add about section
                generators.Add(new StaticHtmlGenerator(dirName, DocumentNames.ABOUT_DOC,
                                                       ReplacementTags.ABOUT_TAG));

                // add education section
                generators.Add(new StaticHtmlGenerator(dirName, DocumentNames.EDUCATION_DOC,
                                                       ReplacementTags.EDUCATION_TAG));

                // add links section
                generators.Add(new LinksGenerator(dirName));

                // add contact section
                generators.Add(new ContactGenerator(dirName, true));

                // add greeting section
                generators.Add(new GreetingGenerator(dirName));

                // add skills section
                generators.Add(new SkillsGenerator(dirName));

                // add xp section
                generators.Add(new ExperienceGenerator(dirName));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        string Combine()
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
            ResumeGenerator fhg = new ResumeGenerator(args);
        }
    }
}
