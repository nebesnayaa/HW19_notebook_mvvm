
namespace HW19_mvvm_notebook.Commands
{
    internal class Note
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return FullName + ' ' + Address + ' ' + Email;
        }

        public Note(string _FullName, string _Address, string _Email)
        {
            FullName = _FullName;
            Address = _Address;
            Email = _Email;
        }
    }
}
