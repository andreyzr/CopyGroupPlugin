using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace CopyGroupPlugin
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference= uiDoc.Selection.PickObject(ObjectType.Element, "Выберете группу элементов");
            Element element= doc.GetElement(reference);
            Group group=element as Group;

            XYZ point = uiDoc.Selection.PickPoint("Выберете точку");

            Transaction transaction = new Transaction(doc);
            transaction.Start("Копирование группы объекта");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;

        }
    }
}
