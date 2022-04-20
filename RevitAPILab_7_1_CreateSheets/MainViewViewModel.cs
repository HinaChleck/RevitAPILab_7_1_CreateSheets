using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
//using RevitAPITrainingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPILab_7_1_CreateSheets
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;
        private Document _doc;
        private FamilySymbol selectedTitleType;
        private int quantity;

        public List<ViewSheet> ViewSheets { get; set; } = new List<ViewSheet>();//списки лучше создавать пустыми, а не null, во избежание ошибок
        public List<FamilySymbol> TitleBlockTypes { get; } = new List<FamilySymbol>();
       // public List<ViewPlan> Views { get; } = new List<ViewPlan>();
        public DelegateCommand SaveCommand { get; }
        public FamilySymbol SelectedTitleType { get => selectedTitleType; set => selectedTitleType = value; }
       // public Level SelectedLevel { get; set; }
        public int Quantity { get => quantity; set => quantity = value; }
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            TitleBlockTypes = TitleBlocksUtils.GetTitleBlockTypes(_commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);
          
        }
        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (SelectedTitleType == null || quantity < 1)
                return;

            using (var ts = new Transaction(doc, "Создать листы"))
            {
                ts.Start();


                for (int i= 0; i < quantity; i++)
                {
                    ViewSheet viewsheet = ViewSheet.Create(doc, SelectedTitleType.Id);
                }


                ts.Commit();
            }
            RaiseCloseRequest();

        }
        public event EventHandler CloseRequest;

        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }

}
