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
            Init();
        }
        public void Init()
        {
            functionals = new Dictionary<string, CMClass>();
            physicals = new Dictionary<string, CMClass>();
            documents = new Dictionary<string, CMClass>();
            attributes = new Dictionary<string, CMAttribute>();
        }
        public void Clear()
        {
            functionals.Clear();
            physicals.Clear();
            documents.Clear();
            attributes.Clear();
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
            // MergeClassTables(functionals, physicals);
        }
        private void MergeClassTables(Dictionary<string, CMClass> recipient, Dictionary<string, CMClass> source)
        {
            if (recipient.Count == 0 || source.Count == 0)
            {
                return;
            }
            foreach (CMClass cmClass in recipient.Values)
            {
                CMClass result = CMClass.FindClassByName(cmClass.Name, source);
                if (result != null)
                {
                    cmClass.AddDescendant(result.Descendants);
                }
                if (cmClass.HasDescendants)
                {
                    bool hasDescendants = cmClass.HasDescendants;
                    Dictionary<string, CMClass> descendants = cmClass.Descendants;
                    while (hasDescendants)
                    {
                        foreach (CMClass descendant in descendants.Values)
                        {
                            result = CMClass.FindClassByName(descendant.Name, source);
                            if (result != null)
                            {
                                cmClass.AddDescendant(result.Descendants);
                            }
                        }

                    }
                }
            }
        }
        public void ExportPermissibleGrid(string fileName)
        {

        }
    }
}
