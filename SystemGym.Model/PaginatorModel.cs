namespace Montreal.Process.Sistel.Models
{
    public class PaginatorModel
    {
        private int page;

        private int pageSize;

        public int Page
        {
            get
            {
                if (this.page == 0)
                {
                    this.page = 1;
                }

                return this.page;
            }

            set
            {
                this.page = value;
            }
        }

        public int PageSize
        {
            get
            {
                if (this.pageSize == 0)
                {
                    this.pageSize = 1000;
                }

                return this.pageSize;
            }

            set
            {
                this.pageSize = value;
            }
        }

        public string Sort { get; set; }

        public string SortDirection { get; set; }
    }
}
