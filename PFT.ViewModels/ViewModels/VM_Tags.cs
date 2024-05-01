using PFT.Base;
using PFT.Data;
using PFT.Interfaces;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {

        //private ICommand _saveTag_ClickCommand;
        //public ICommand SaveTag_ClickCommand
        //{
        //    get
        //    {
        //        return _saveTag_ClickCommand ?? (_saveTag_ClickCommand = new CommandHandler(() => SaveNewTag(CurrentTag), _canExecute));
        //    }
        //}
        public void SaveNewTag(Tag tag)
        {
            ITagData td = new TagData();
            //tag.ParentTagId = CurrentTag.Id;
            td.Save(tag);
            GetAllTags();

            //System.Windows.MessageBox.Show("Saved");            
        }

        //private ICommand _deleteTag_ClickCommand;
        //public ICommand DeleteTag_ClickCommand
        //{
        //    get
        //    {
        //        return _deleteTag_ClickCommand ?? (_deleteTag_ClickCommand = new CommandHandler(() => DeleteTag(CurrentTag), _canExecute));
        //    }
        //}
        public void DeleteTag(Tag tag)
        {
            ITagData td = new TagData();
            td.Delete(tag);
            GetAllTags();

            //System.Windows.MessageBox.Show("Deleted Tag");
        }

        //private ICommand _newTag_ClickCommand;
        //public ICommand NewTag_ClickCommand
        //{
        //    get
        //    {
        //        return _newTag_ClickCommand ?? (_newTag_ClickCommand = new CommandHandler(() => CreateNewTag(), _canExecute));
        //    }
        //}
        public void CreateNewTag()
        {
            CurrentTag.Id = -1;
            CurrentTag.Name = string.Empty;
            CurrentTag.Description = string.Empty;
            CurrentTag.ParentTagId = -1;
            //System.Windows.MessageBox.Show("New Tag");
        }
        




        private Tag _currentTag;
        public Tag CurrentTag
        {
            get {
                if (_currentTag == null)
                    _currentTag = new Tag();

                return _currentTag; }
            set { 
                _currentTag = value;
            }
        }


        private TagCol _tags;
        public TagCol Tags
        {
            get {
                if (_tags == null)
                    _tags = new TagCol();

                return _tags; 
            }
            set {
                _tags = value;
            }
        }

        private void GetAllTags()
        {
            Tags.Clear();

            ITagColData tcd = new TagColData();
            foreach (Tag tag in tcd.LoadAll())
            {
                Tags.Add(tag);
            }
        }
    }
}
