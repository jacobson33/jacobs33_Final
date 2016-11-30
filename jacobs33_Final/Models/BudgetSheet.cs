using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jacobs33_Final.Models
{
    public class BudgetSheet
    {
        #region Enum
        #endregion

        #region Fields
        private string _name;
        private string _description;
        private List<LineItem> _budgetItems;
        private DateTime _dateCreated;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public List<LineItem> BudgetItems
        {
            get { return _budgetItems; }
            set { _budgetItems = value; }
        }
        public DateTime DateCreated
        {
            get { return _dateCreated; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// BudgetSheet constructor
        /// </summary>
        public BudgetSheet()
        {
            _budgetItems = new List<LineItem>();
            _dateCreated = DateTime.Now;
        }
        #endregion

        #region Methods
        public void SaveBudgetSheet()
        {

        }

        public void LoadBudgetSheet()
        {

        }
        #endregion
    }
}
