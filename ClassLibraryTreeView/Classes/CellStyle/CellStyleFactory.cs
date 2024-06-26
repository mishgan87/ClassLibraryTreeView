﻿using ClosedXML.Excel;

namespace ClassLibraryTreeView.Classes.CellStyle
{
    public enum CellStyle
    {
        Default = 0,
        Empty = 1,
        Class = 2,
        ClassId = 3,
        Attribute = 4,
        AttributesGroup = 5,
        Discipline = 6,
        Header = 7,
        PresenceUnselect = 8,
        PresenceNonApplicable = 9,
        PresenceOptional = 10,
        PresencePreffered = 11,
        PresenceRequired = 12
    }
    public class CellStyleFactory
    {
        public CellStyleFactory()
        {

        }
        public IXLStyle CreateCellStyleForClass()
        {
            return new CellStyleForClass();
        }
        public IXLStyle CreateCellStyleForClassDark()
        {
            return new CellStyleForClassDark();
        }
        public IXLStyle CreateCellStyleForClassId()
        {
            return new CellStyleForClassId();
        }
        public IXLStyle CreateCellStyleForAttribute()
        {
            return new CellStyleForAttribute();
        }
        public IXLStyle CreateCellStyleForAttributesGroup()
        {
            return new CellStyleForAttributesGroup();
        }
        public IXLStyle CreateCellStyleForDiscipline()
        {
            return new CellStyleForDiscipline();
        }
        public IXLStyle CreateCellStyleForHeader()
        {
            return new CellStyleForHeader();
        }
        public IXLStyle CreateCellStyleForAttributePresence(string presence)
        {
            switch (presence)
            {
                case "":
                    return CreateCellStyleForPresenceNonApplicable();
                case "X":
                    return CreateCellStyleForPresenceUnselect();
                case "O":
                    return CreateCellStyleForPresenceOptional();
                case "P":
                    return CreateCellStyleForPresencePreffered();
                case "R":
                    return CreateCellStyleForPresenceRequired();
                default:
                    return CreateCellStyleDeafult();
            }
        }
        public IXLStyle CreateCellStyleForPresenceUnselect()
        {
            return new CellStyleForPresenceUnselect();
        }
        public IXLStyle CreateCellStyleForPresenceNonApplicable()
        {
            return new CellStyleForPresenceNonApplicable();
        }
        public IXLStyle CreateCellStyleForPresenceOptional()
        {
            return new CellStyleForPresenceOptional();
        }
        public IXLStyle CreateCellStyleForPresencePreffered()
        {
            return new CellStyleForPresencePreffered();
        }
        public IXLStyle CreateCellStyleForPresenceRequired()
        {
            return new CellStyleForPresenceRequired();
        }
        public IXLStyle CreateCellStyleDeafult()
        {
            return new CellStyleDefault();
        }
        public IXLStyle CreateCellStyle(CellStyle cellStyle)
        {
            switch (cellStyle)
            {
                case CellStyle.Class:
                    return new CellStyleForClass();

                case CellStyle.ClassId:
                    return new CellStyleForClassId();

                case CellStyle.Attribute:
                    return new CellStyleForAttribute();

                case CellStyle.AttributesGroup:
                    return new CellStyleForAttributesGroup();

                case CellStyle.Discipline:
                    return new CellStyleForDiscipline();

                case CellStyle.Header:
                    return new CellStyleForHeader();

                case CellStyle.PresenceUnselect:
                    return new CellStyleForPresenceUnselect();

                case CellStyle.PresenceNonApplicable:
                    return new CellStyleForPresenceNonApplicable();

                case CellStyle.PresenceOptional:
                    return new CellStyleForPresenceOptional();

                case CellStyle.PresencePreffered:
                    return new CellStyleForPresencePreffered();

                case CellStyle.PresenceRequired:
                    return new CellStyleForPresenceRequired();

                default:
                    return new CellStyleDefault();
            }
        }
    }
}
