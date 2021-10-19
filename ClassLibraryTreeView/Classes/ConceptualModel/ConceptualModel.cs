using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;
using System;

namespace ClassLibraryTreeView
{
    public partial class ConceptualModel
    {
        public Dictionary<string, Dictionary<string, ConceptualModelClass>> classes = new Dictionary<string, Dictionary<string, ConceptualModelClass>>();
        public Dictionary<string, Dictionary<string, ConceptualModelAttribute>> attributes = new Dictionary<string, Dictionary<string, ConceptualModelAttribute>>();

        public Dictionary<string, ConceptualModelTaxonomy> taxonomies = new Dictionary<string, ConceptualModelTaxonomy>();
        public Dictionary<string, ConceptualModelMeasureUnit> measureUnits = new Dictionary<string, ConceptualModelMeasureUnit>();
        public Dictionary<string, ConceptualModelMeasureClass> measureClasses = new Dictionary<string, ConceptualModelMeasureClass>();
        public Dictionary<string, ConceptualModelEnumeration> enumerations = new Dictionary<string, ConceptualModelEnumeration>();

        public int AttributesCount
        { 
            get
            {
                int count = 0;
                if (attributes.Count > 0)
                {
                    foreach (KeyValuePair<string, Dictionary<string, ConceptualModelAttribute>> group in attributes)
                    {
                        count += group.Value.Count;
                    }
                }
                return count;
            }
        }
        public string FullPathXml { get; set; }
        public string ModelName { get; set; }
        public Dictionary<string, ConceptualModelTaxonomy> Taxonomies => taxonomies;
        public Dictionary<string, ConceptualModelEnumeration> Enumerations => enumerations;
        public Dictionary<string, ConceptualModelMeasureUnit> ConceptualModelMeasureUnits => measureUnits;
        public Dictionary<string, ConceptualModelMeasureClass> MeasureClasses => measureClasses;
        public Dictionary<string, ConceptualModelClass> MergedClasses
        {
            get
            {
                Dictionary<string, ConceptualModelClass> map = Physicals;
                if (map.Count == 0)
                {
                    map = Functionals;
                }
                return map;
            }
        }
        public Dictionary<string, ConceptualModelClass> Functionals
        {
            get
            {
                if (!classes.ContainsKey("functionals"))
                {
                    classes.Add("functionals", new Dictionary<string, ConceptualModelClass>());
                }
                return classes["functionals"];
            }
        }
        public Dictionary<string, ConceptualModelClass> Physicals
        {
            get
            {
                if (!classes.ContainsKey("physicals"))
                {
                    classes.Add("physicals", new Dictionary<string, ConceptualModelClass>());
                }
                return classes["physicals"];
            }
        }

        public ConceptualModel()
        {
            Init();
        }
        public void Init()
        {
            classes = new Dictionary<string, Dictionary<string, ConceptualModelClass>>();
            attributes = new Dictionary<string, Dictionary<string, ConceptualModelAttribute>>();
            taxonomies = new Dictionary<string, ConceptualModelTaxonomy>();
            measureClasses = new Dictionary<string, ConceptualModelMeasureClass>();
            measureUnits = new Dictionary<string, ConceptualModelMeasureUnit>();
            enumerations = new Dictionary<string, ConceptualModelEnumeration>();
        }
        public void Clear()
        {
            classes.Clear();
            attributes.Clear();
            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();
        }
        public bool OpenFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    FullPathXml = filename;
                    ImportXml(filename);
                    filename = filename.Remove(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));
                    filename = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);
                    ModelName = $"{filename}";
                }
            }
            return true;
        }
        private void DefinePermissibleAttributesNames(Dictionary<string, Dictionary<string, ConceptualModelClass>> classesMaps,
                                                        Dictionary<string, Dictionary<string, ConceptualModelAttribute>> conceptualModelAttributes)
        {
            foreach (var classesMap in classesMaps.Values)
            {
                if (classesMap.Count > 0)
                {
                    foreach (ConceptualModelClass cmClass in classesMap.Values)
                    {
                        if (cmClass.PermissibleAttributes.Count > 0)
                        {
                            foreach (var (permissibleAttribute, suitableAttribute) in from KeyValuePair<string, ConceptualModelAttribute> permissibleAttribute in cmClass.PermissibleAttributes
                                                                                      let suitableAttributes = from cmAttributeGroup in conceptualModelAttributes.Values
                                                                                                               where cmAttributeGroup.ContainsKey(permissibleAttribute.Key)
                                                                                                               select cmAttributeGroup[permissibleAttribute.Key]
                                                                                      where suitableAttributes.Count() > 0
                                                                                      let suitableAttribute = suitableAttributes.First()
                                                                                      select (permissibleAttribute, suitableAttribute))
                            {
                                permissibleAttribute.Value.Description = suitableAttribute.Description;
                                permissibleAttribute.Value.Name = suitableAttribute.Name;
                                permissibleAttribute.Value.AddApplicableClass(cmClass);
                                suitableAttribute.AddApplicableClass(cmClass);
                            }
                        }
                    }
                }
            }
        }
        private static void MergeAttributes(ConceptualModelClass cmClassSource,
                                            ConceptualModelClass cmClassRecipient,
                                            Dictionary<string, Dictionary<string, ConceptualModelAttribute>> conceptualModelAttributes)
        {
            if (cmClassSource.PermissibleAttributes.Count == 0)
            {
                return;
            }

            foreach (ConceptualModelAttribute cmClassSourceAttribute in cmClassSource.PermissibleAttributes.Values)
            {
                if (!cmClassRecipient.PermissibleAttributes.ContainsKey(cmClassSourceAttribute.Id))
                {
                    ConceptualModelAttribute newAttribute = new ConceptualModelAttribute(cmClassSourceAttribute);
                    if (cmClassSourceAttribute.CameFrom != null)
                    {
                        newAttribute.CameFrom = cmClassSourceAttribute.CameFrom;
                    }
                    else
                    {
                        newAttribute.CameFrom = cmClassSource;
                    }
                    newAttribute.AddApplicableClass(cmClassRecipient);
                    cmClassRecipient.PermissibleAttributes.Add(newAttribute.Id, newAttribute);
                    if (conceptualModelAttributes.ContainsKey(newAttribute.Id))
                    {
                        IEnumerable<ConceptualModelAttribute> suitableAttributes = from cmAttributeGroup in conceptualModelAttributes.Values
                                                                                   where cmAttributeGroup.ContainsKey(newAttribute.Id)
                                                                                   select cmAttributeGroup[newAttribute.Id];
                        ConceptualModelAttribute suitableAttribute = suitableAttributes.First();
                        suitableAttribute.AddApplicableClass(cmClassRecipient);
                    }
                }
            }
        }
        private static void SetClassesInheritance(Dictionary<string,
                                                    Dictionary<string, ConceptualModelClass>> classesMaps,
                                                    Dictionary<string, Dictionary<string, ConceptualModelAttribute>> conceptualModelAttributes)
        {
            foreach (Dictionary<string, ConceptualModelClass> map in classesMaps.Values)
            {
                for (int index = 0; index < map.Values.Count; index++)
                {
                    ConceptualModelClass cmClass = map.Values.ElementAt(index);
                    if (!cmClass.Extends.Equals("") && !cmClass.Extends.ToLower().Equals("not found"))
                    {
                        var parent = map[cmClass.Extends];
                        cmClass.Parent = map[cmClass.Extends];
                        MergeAttributes(cmClass.Parent, cmClass, conceptualModelAttributes);
                        map[cmClass.Extends].Children.Add(cmClass.Id, cmClass);
                    }
                }
            }
        }
        public void ImportXml(string fileName)
        {
            Func<XElement, string, IEnumerable<XElement>> GetElements = (xRootElement, text) =>
            {
                return from xElement in xRootElement.Elements() where xElement.Name.LocalName.ToLower().Equals(text) select xElement;
            };

            Func<string, XElement, Dictionary<string, ConceptualModelClass>> GetClasses = (concept, xRootElement) =>
            {
                IEnumerable<XElement> rootOfClasses = GetElements(xRootElement, concept);

                IEnumerable<ConceptualModelClass> classesCollection = from xElement in rootOfClasses.Elements()
                                                                      where !xElement.Name.LocalName.ToLower().Equals("extension")
                                                                      select new ConceptualModelClass(xElement);
                return classesCollection.ToDictionary(cmClass => cmClass.Id);
            };

            Clear();
            FullPathXml = fileName;
            XDocument doc = XDocument.Load(fileName);
            XElement docRoot = doc.Elements().First();

            // get conceptual model objects attributes collection
            
            IEnumerable<ConceptualModelAttribute> attributesAll = from xElement in GetElements(docRoot, "attributes").Elements()
                                                                      where !xElement.Name.LocalName.ToLower().Equals("extension")
                                                                      select new ConceptualModelAttribute(xElement);

            foreach (ConceptualModelAttribute conceptualModelAttribute in attributesAll)
            {
                string group = conceptualModelAttribute.Group;
                if (!this.attributes.ContainsKey(group))
                {
                    this.attributes.Add(group, new Dictionary<string, ConceptualModelAttribute>());
                }
                this.attributes[group].Add(conceptualModelAttribute.Id, conceptualModelAttribute);
            }

            // get conceptual model classes

            classes.Add("functionals", GetClasses("functionals", docRoot));
            classes.Add("physicals", GetClasses("physicals", docRoot));
            classes.Add("documents", GetClasses("documents", docRoot));
            classes.Add("generals", GetClasses("generals", docRoot));

            // get conceptual model reference data

            var rootOfRefenceData = GetElements(docRoot, "referencedata").First();

            IEnumerable<ConceptualModelTaxonomy> taxonomiesCollection = from xElement in GetElements(rootOfRefenceData, "taxonomies").Elements()
                                                                        where xElement.Name.LocalName.ToLower().Equals("taxonomy")
                                                                        select new ConceptualModelTaxonomy(xElement);

            IEnumerable<ConceptualModelEnumeration> enumerationsCollection = from xElement in GetElements(rootOfRefenceData, "enumerations").Elements()
                                                                        where xElement.Name.LocalName.ToLower().Equals("list")
                                                                        select new ConceptualModelEnumeration(xElement);

            IEnumerable<XElement> uomElements = from xElement in GetElements(rootOfRefenceData, "uom").Elements()
                                                where xElement.Name.LocalName.ToLower().Equals("units") ||
                                                xElement.Name.LocalName.ToLower().Equals("measureclasses")
                                                select xElement;

            IEnumerable<ConceptualModelMeasureUnit> measureUnitsCollection = from xElement in uomElements.Elements()
                                                                             where xElement.Name.LocalName.ToLower().Equals("unit")
                                                                             select new ConceptualModelMeasureUnit(xElement);

            IEnumerable<ConceptualModelMeasureClass> measureClassesCollection = from xElement in uomElements.Elements()
                                                                             where xElement.Name.LocalName.ToLower().Equals("measureclass")
                                                                             select new ConceptualModelMeasureClass(xElement);

            this.taxonomies = taxonomiesCollection.ToDictionary(cmTax => cmTax.Id);
            this.enumerations = enumerationsCollection.ToDictionary(cmList => cmList.Id);
            this.measureUnits = measureUnitsCollection.ToDictionary(cmMeasureUnit => cmMeasureUnit.Id);
            this.measureClasses = measureClassesCollection.ToDictionary(cmMeasureClass => cmMeasureClass.Id);

            DefinePermissibleAttributesNames(classes, attributes);
            SetClassesInheritance(classes, attributes);

            if (Physicals == null)
            {
                return;
            }

            foreach (var physicalClass in
            // merge permissible attributes of roots functional and physical classes
            from ConceptualModelClass physicalClass in classes["physicals"].Values
            where physicalClass.Parent == null
            select physicalClass)
            {
                foreach (var functionalClass in from ConceptualModelClass functionalClass in classes["functionals"].Values
                                                where functionalClass.Parent == null
                                                select functionalClass)
                {
                    MergeAttributes(functionalClass, physicalClass, attributes);
                    break;
                }

                break;
            }

            // merge permissible attributes of functional and physical classes (classes merged by name)

            foreach (ConceptualModelClass physicalClass in classes["physicals"].Values)
            {
                foreach (ConceptualModelClass functionalClass in classes["functionals"].Values)
                {
                    if (functionalClass.Name.Equals(physicalClass.Name))
                    {
                        if (functionalClass.PermissibleAttributes.Count > 0)
                        {
                            MergeAttributes(functionalClass, physicalClass, attributes);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach(ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClass, physicalClassChild, attributes);
                                }
                            }
                        }

                        ConceptualModelClass physicalClassParent = physicalClass.Parent;
                        while (physicalClassParent != null)
                        {
                            MergeAttributes(physicalClassParent, physicalClass, attributes);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(physicalClassParent, physicalClassChild, attributes);
                                }
                            }

                            physicalClassParent = physicalClassParent.Parent;
                        }

                        ConceptualModelClass functionalClassParent = functionalClass.Parent;
                        while (functionalClassParent != null)
                        {
                            MergeAttributes(functionalClassParent, physicalClass, attributes);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClassParent, physicalClassChild, attributes);
                                }
                            }

                            functionalClassParent = functionalClassParent.Parent;
                        }
                    }
                }
            }
        }
        public ConceptualModelClass GetClass(string id)
        {
            foreach(var map in classes.Values)
            {
                if (map.ContainsKey(id))
                {
                    return map[id];
                }
            }
            return null;
        }
        public static string[] SplitValidationRules(string validationRule)
        {
            List<string> rules = new List<string>();

            string rule = validationRule;
            rule = rule.Remove(rule.IndexOf("::"), rule.Length - rule.IndexOf("::"));
            rules.Add(rule);

            string concept = validationRule;
            concept = concept.Remove(0, concept.IndexOf("::") + 2);
            concept = concept.Remove(concept.IndexOf("::"), concept.Length - concept.IndexOf("::"));
            rules.Add(concept);

            string ids = validationRule;
            ids = ids.Remove(0, ids.LastIndexOf("::") + 2);

            while (ids.Length > 0)
            {
                string id = ids;
                if (id.Contains("||"))
                {
                    id = id.Remove(id.IndexOf("||"), id.Length - id.IndexOf("||"));

                    ids = ids.Remove(0, ids.IndexOf("||") + 2);
                }
                else
                {
                    ids = ids.Remove(0, ids.Length);
                }

                rules.Add(id);
            }

            return rules.ToArray();
        }
        private void MergeByAssociations()
        {
            if (classes.ContainsKey("merged"))
            {
                classes.Remove("merged");
            }
            classes.Add("merged", new Dictionary<string, ConceptualModelClass>());
            Dictionary<string, ConceptualModelClass> merged = classes["merged"];
            foreach (ConceptualModelClass cmClass in classes["functionals"].Values)
            {
                if (!cmClass.Extends.Equals(""))
                {
                    // continue;
                }

                if (!merged.Values.Contains(cmClass))
                {
                    merged.Add(cmClass.Id, cmClass);
                }

                foreach (ConceptualModelAttribute attribute in cmClass.PermissibleAttributes.Values)
                {
                    if (attribute.ValidationType.ToLower().Equals("association"))
                    {
                        string[] rules = SplitValidationRules(attribute.ValidationRule);
                        string concept = rules[1];

                        for (int index = 2; index < rules.Length; index++)
                        {
                            if (concept.ToLower().Equals("functional"))
                            {
                                merged.Add(classes["functionals"][rules[index]].Id, classes["functionals"][rules[index]]);
                            }
                            if (concept.ToLower().Equals("physical"))
                            {
                                merged.Add(classes["physicals"][rules[index]].Id, classes["physicals"][rules[index]]);
                            }
                        }
                    }
                }
            }
        }
    }
}
