using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ClassLibraryTreeView
{
    public class ConceptualModel
    {
        public List<CMClass> classes;
        public List<string> classesTypes;
        public List<CMAttribute> attributes;

        public Dictionary<string, List<CMClass>> classMap;

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            classMap = new Dictionary<string, List<CMClass>>();

            classes = new List<CMClass>();
            classesTypes = new List<string>();
            attributes = new List<CMAttribute>();
        }
        public void Clear()
        {
            classes.Clear();
            attributes.Clear();
            classesTypes.Clear();

            classMap.Clear();
        }
        public void ImportXml(string fileName)
        {
            Clear();
            XDocument doc = XDocument.Load(fileName);
            foreach(XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName;

                if (name.ToLower().Equals("functionals") || name.ToLower().Equals("physicals") || name.ToLower().Equals("documents"))
                {
                    classesTypes.Add(name);
                    CMClass.AddClasses(classMap/*classes*/, element);
                }

                if (name.ToLower().Equals("attributes"))
                {
                    CMAttribute.AddAttributes(attributes, element);
                }
            }
        }
    }
}
