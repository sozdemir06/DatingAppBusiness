namespace DatingApp.Core.Utilities.Helpers.AppHeaderHelper
{
    public class UserParams
    {
        private int MaxPageSize=50;
        public int PageNumber { get; set; }=1;
        private int pageSize=10;
        public int PageSize 
        { 
            get{return pageSize;}
            set{pageSize=(value>MaxPageSize)?MaxPageSize:value;}
        }
    }
}