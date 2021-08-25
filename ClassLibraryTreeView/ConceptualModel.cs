using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        public Dictionary<string, CMClass> functionals;
        public Dictionary<string, CMClass> physicals;
        public Dictionary<string, CMClass> documents;
        public Dictionary<string, CMAttribute> attributes;

        public ConceptualModel()
        {
            functionals = new Dictionary<string, CMClass>();
            physicals = new Dictionary<string, CMClass>();
            documents = new Dictionary<string, CMClass>();
            attributes = new Dictionary<string, CMAttribute>();
        }
        public void Clear()
        {
            functionals.Clear();
            attributes.Clear();
            physicals.Clear();
            documents.Clear();
        }
        public void ImportXml(string fileName)
        {
            Clear();
            XDocument doc = XDocument.Load(fileName);
            foreach(XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName.ToLower();
                if (name.Equals("functionals"))
                {
                    functionals = CMClass.FillClassMap(element);
                }
                if (name.Equals("physicals"))
                {
                    physicals = CMClass.FillClassMap(element);
                }
                if (name.Equals("documents"))
                {
                    documents = CMClass.FillClassMap(element);
                }
            }
        }
    }
}
