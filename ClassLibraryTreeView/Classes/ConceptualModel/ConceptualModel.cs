using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryTreeView.Interfaces;
using ClassLibraryTreeView.Classes;

namespace ClassLibraryTreeView
{
    public partial class ConceptualModel
    {
        public Dictionary<string, Dictionary<string, ConceptualModelClass>> classes = new Dictionary<string, Dictionary<string, ConceptualModelClass>>();
        public Dictionary<string, Dictionary<string, ConceptualModelAttribute>> attributes = new Dictionary<string, Dictionary<string, ConceptualModelAttribute>>();

        public HashSet<ConceptualModelTaxonomy> taxonomies = new HashSet<ConceptualModelTaxonomy>();
        public Dictionary<string, ConceptualModelMeasureUnit> measureUnits = new Dictionary<string, ConceptualModelMeasureUnit>();
        public Dictionary<string, ConceptualModelMeasureClass> measureClasses = new Dictionary<string, ConceptualModelMeasureClass>();
        public Dictionary<string, ConceptualModelEnumeration> enumerations = new Dictionary<string, ConceptualModelEnumeration>();


        

        public int AttributesCount { get; set; }
        public string FullPathXml { get; set; }
        public string ModelName { get; set; }
        // public Dictionary<string, ConceptualModelTaxonomy> Taxonomies => taxonomies;
        public HashSet<ConceptualModelTaxonomy> Taxonomies => taxonomies;
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

            taxonomies = new HashSet<ConceptualModelTaxonomy>();
            measureClasses = new Dictionary<string, ConceptualModelMeasureClass>();
            measureUnits = new Dictionary<string, ConceptualModelMeasureUnit>();
            enumerations = new Dictionary<string, ConceptualModelEnumeration>();

            AttributesCount = 0;
        }
        public void Clear()
        {
            AttributesCount = 0;

            classes.Clear();
            attributes.Clear();
            taxonomies.Clear();
            enumerations.Clear();
            measureUnits.Clear();
            measureClasses.Clear();
        }
        public ConceptualModelAttribute GetAttributeById(string id)
        {
            ConceptualModelAttribute attribute = null;
            foreach (string group in attributes.Keys)
            {
                // if (attributes[group].ContainsKey(id))
                if (attributes[group].TryGetValue(id, out attribute))
                {
                    // return attributes[group][id];
                    break;
                }
            }
            // return null;
            return attribute;
        }
        private void GetUoM(XElement referenceDataElement)
        {
            foreach (XElement element in referenceDataElement.Elements())
            {
                string name = element.Name.LocalName.ToLower();

                if (name.Equals("units"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        ConceptualModelMeasureUnit measureUnit = new ConceptualModelMeasureUnit(child);
                        measureUnits.Add(measureUnit.Id, measureUnit);
                    }
                }

                if (name.Equals("measureclasses"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        ConceptualModelMeasureClass measureClass = new ConceptualModelMeasureClass(child);
                        measureClasses.Add(measureClass.Id, measureClass);
                    }
                }
            }
        }

        private void GetReferenceData(XElement referenceDataElement)
        {
            foreach (XElement element in referenceDataElement.Elements())
            {
                string name = element.Name.LocalName;
                if (name.ToLower().Equals("enumerations"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        ConceptualModelEnumeration enumerationList = new ConceptualModelEnumeration(child);
                        enumerations.Add(enumerationList.Id, enumerationList);
                    }
                }

                if (name.ToLower().Equals("uom"))
                {
                    GetUoM(element);
                }
                if (name.ToLower().Equals("taxonomies"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        ConceptualModelTaxonomy taxonomy = new ConceptualModelTaxonomy(child);
                        taxonomies.Add(taxonomy);
                    }
                }
            }
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
        private void DefinePermissibleAttributesNames()
        {
            foreach (Dictionary<string, ConceptualModelClass> map in classes.Values)
            {
                if (map.Count > 0)
                {
                    foreach (ConceptualModelClass cmClass in map.Values)
                    {
                        if (cmClass.PermissibleAttributes.Count > 0)
                        {
                            foreach (ConceptualModelAttribute cmClassAttribute in cmClass.PermissibleAttributes.Values)
                            {
                                ConceptualModelAttribute attribute = GetAttribute(cmClassAttribute.Id);
                                if (attribute != null)
                                {
                                    attribute.AddApplicableClass(cmClass);
                                }
                                if (cmClassAttribute != null)
                                {
                                    cmClassAttribute.AddApplicableClass(cmClass);
                                }
                                if (cmClassAttribute.Name.Equals(""))
                                {
                                    foreach (string group in attributes.Keys)
                                    {
                                        if (attributes[group].ContainsKey(cmClassAttribute.Id))
                                        {
                                            cmClassAttribute.Name = attributes[group][cmClassAttribute.Id].Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void MergeAttributes(ConceptualModelClass cmClassSource, ConceptualModelClass cmClassRecipient)
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
                    cmClassRecipient.PermissibleAttributes.Add(newAttribute.Id, newAttribute);
                    ConceptualModelAttribute attribute = GetAttributeById(newAttribute.Id);
                    if (attribute != null)
                    {
                        attribute.AddApplicableClass(cmClassRecipient);
                    }
                    if (newAttribute != null)
                    {
                        newAttribute.AddApplicableClass(cmClassRecipient);
                    }
                }
            }
        }
        private void SetClassesInheritance()
        {
            foreach (Dictionary<string, ConceptualModelClass> map in classes.Values)
            {
                for (int index = 0; index < map.Values.Count; index++)
                {
                    ConceptualModelClass cmClass = map.Values.ElementAt(index);
                    if (!cmClass.Extends.Equals("") && !cmClass.Extends.ToLower().Equals("not found"))
                    {
                        var parent = map[cmClass.Extends];
                        cmClass.Parent = map[cmClass.Extends];
                        MergeAttributes(cmClass.Parent, cmClass);
                        map[cmClass.Extends].Children.Add(cmClass.Id, cmClass);
                    }
                }
            }
        }
        public void ImportXml(string fileName)
        {
            Clear();
            FullPathXml = fileName;
            XDocument doc = XDocument.Load(fileName);
            foreach (XElement element in doc.Elements().First().Elements())
            {
                string name = element.Name.LocalName.ToLower();

                if (name.Equals("referencedata"))
                {
                    GetReferenceData(element);
                }

                if (name.Equals("functionals") || name.Equals("physicals") || name.Equals("documents"))
                {
                    classes.Add(name, new Dictionary<string, ConceptualModelClass>());
                    foreach (XElement child in element.Elements())
                    {
                        if (!child.Name.LocalName.ToLower().Equals("extension"))
                        {
                            ConceptualModelClass newClass = new ConceptualModelClass(child);
                            classes[name].Add(newClass.Id, newClass);
                        }
                    }
                }

                if (name.Equals("attributes"))
                {
                    foreach (XElement child in element.Elements())
                    {
                        ConceptualModelAttribute newAttribute = new ConceptualModelAttribute(child);

                        string group = newAttribute.Group;

                        if (group.Equals(""))
                        {
                            group = "Unset";
                        }

                        if (!attributes.ContainsKey(group))
                        {
                            attributes.Add(group, new Dictionary<string, ConceptualModelAttribute>());
                        }

                        attributes[group].Add(newAttribute.Id, newAttribute);
                        AttributesCount++;
                    }
                }
            }

            DefinePermissibleAttributesNames();
            SetClassesInheritance();

            if (Physicals == null)
            {
                return;
            }

            // merge permissible attributes of roots functional and physical classes

            foreach (ConceptualModelClass physicalClass in classes["physicals"].Values)
            {
                if (physicalClass.Parent == null)
                {
                    foreach (ConceptualModelClass functionalClass in classes["functionals"].Values)
                    {
                        if (functionalClass.Parent == null)
                        {
                            MergeAttributes(functionalClass, physicalClass);
                            break;
                        }
                    }
                    break;
                }
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
                            MergeAttributes(functionalClass, physicalClass);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach(ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClass, physicalClassChild);
                                }
                            }
                        }

                        ConceptualModelClass physicalClassParent = physicalClass.Parent;
                        while (physicalClassParent != null)
                        {
                            MergeAttributes(physicalClassParent, physicalClass);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(physicalClassParent, physicalClassChild);
                                }
                            }

                            physicalClassParent = physicalClassParent.Parent;
                        }

                        ConceptualModelClass functionalClassParent = functionalClass.Parent;
                        while (functionalClassParent != null)
                        {
                            MergeAttributes(functionalClassParent, physicalClass);

                            if (physicalClass.Children.Count > 0)
                            {
                                foreach (ConceptualModelClass physicalClassChild in physicalClass.Children.Values)
                                {
                                    MergeAttributes(functionalClassParent, physicalClassChild);
                                }
                            }

                            functionalClassParent = functionalClassParent.Parent;
                        }
                    }
                }
            }
        }
        public ConceptualModelAttribute GetAttributeByName(string name)
        {
            foreach (string group in attributes.Keys)
            {
                foreach (ConceptualModelAttribute attribute in attributes[group].Values)
                {
                    if (attribute.Name.Equals(name))
                    {
                        return attribute;
                    }
                }
            }
            return null;
        }
        public ConceptualModelAttribute GetAttribute(string id)
        {
            foreach (string group in attributes.Keys)
            {
                if (attributes[group].ContainsKey(id))
                {
                    return attributes[group][id];
                }
            }
            return null;
        }
        public ConceptualModelAttribute GetAttribute(int number)
        {
            int col = 0;
            foreach (string group in attributes.Keys)
            {
                foreach (ConceptualModelAttribute attribute in attributes[group].Values)
                {
                    if (col == number)
                    {
                        return attribute;
                    }
                    col++;
                }
            }
            return null;
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
