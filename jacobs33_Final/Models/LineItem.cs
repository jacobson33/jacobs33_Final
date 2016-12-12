using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jacobs33_Final.Models
{
    public class LineItem
    {
        #region Enum
        public enum LineItemType
        {
            None, Income, Expense, Other
        }
        #endregion

        #region Fields
        private string _name;
        private string _description;
        private LineItemType _itemType;
        private double _itemAmount;
        private int _id;
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
        public LineItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }
        public double ItemAmount
        {
            get { return _itemAmount; }
            set { _itemAmount = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// LineItem constructor
        /// </summary>
        public LineItem()
        {

        }
        /// <summary>
        /// Overload for LineItem constructor
        /// </summary>
        /// <param name="name">Name of Line Item</param>
        /// <param name="type">Type of Line Item</param>
        /// <param name="amount">Line Item amount</param>
        public LineItem(string name, LineItemType type, double amount)
        {
            _name = name;
            _itemType = type;
            _itemAmount = amount;
        }

        #endregion

        #region Methods

        #endregion
    }
}
