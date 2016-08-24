using GlobalLogic.BLL.Factory.WriterFactory;
using GlobalLogic.BLL.Model;
using GlobalLogic.Helper;
using GlobalLogic_Assignment;
using System;
using System.Windows;
using System.Windows.Input;

namespace GlobalLogic.Dialog.ViewModel
{
    class PostDetailVM : NotifyPropertyChanged
    {
        #region "- Local Variables -"
        ILogger logger = new LogService(typeof(PostDetailVM));
        private App app = Application.Current as App;
        #endregion

        #region "- Public properties -"
        private WriterType _selectedWriter;
        public WriterType SelectedWriter
        {
            get
            {
                return _selectedWriter;
            }
            set
            {
                _selectedWriter = value;
                this.PostDetail();
                OnPropertyChanged();
            }
        }

        private string _postContent;
        public string PostContent
        {
            get
            {
                return _postContent;
            }
            set
            {
                _postContent = value;
                OnPropertyChanged();
            }
        }

        private Post _postData = new Post();
        public Post PostData
        {
            get
            {
                return _postData;
            }
            set
            {
                _postData = value;
                OnPropertyChanged();
            }
        }

        #endregion
                
        #region "- ICommand -"

        //private DelegateParameterizedCommand<object> generatePostDataCommand;
        //public ICommand GeneratePostDataCommand
        //{
        //    get
        //    {
        //        if (this.generatePostDataCommand == null)
        //        {
        //            this.generatePostDataCommand = new DelegateParameterizedCommand<object>(this.PostDetail);
        //        }
        //        return this.generatePostDataCommand;
        //    }
        //}

        private DelegateParameterizedCommand<object> clearPostDataCommand;
        public ICommand ClearPostDataCommand
        {
            get
            {
                if (this.clearPostDataCommand == null)
                {
                    this.clearPostDataCommand = new DelegateParameterizedCommand<object>(this.ClearPostDetail);
                }
                return this.clearPostDataCommand;
            }
        }

        private DelegateParameterizedCommand<object> copyPostDataCommand;
        public ICommand CopyPostDataCommand
        {
            get
            {
                if (this.copyPostDataCommand == null)
                {
                    this.copyPostDataCommand = new DelegateParameterizedCommand<object>(this.CopyPostDetails);
                }
                return this.copyPostDataCommand;
            }
        }

        #endregion

        #region "- Handler -"

        public void PostDetail(object data = null)
        {
            try
            {
                if (PostData != null)
                {
                    IFileWriter writer = WriterFactory.getWriter(SelectedWriter);
                    PostContent = writer.Generate<Post>(PostData);

                    app.StatusBarContent = String.Format("Post Details generated successfully in {0} format.",SelectedWriter.ToString());
                }
            }
            catch (Exception ex)
            {
                app.StatusBarContent = String.Format("Error in generating the post details in {0} format. Please check error log and contact the support team.", SelectedWriter.ToString());
                logger.Error(String.Format("Error in generating the post details. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
            }
        }

        private void CopyPostDetails(object data = null)
        {
            try
            {
                if (PostContent != string.Empty)
                    Clipboard.SetDataObject(PostContent);

                app.StatusBarContent = "Post Details copied successfully.";
            }
            catch (Exception ex)
            {
                app.StatusBarContent = "Error in copying the post details. Please check error log and contact the support team. ";
                logger.Error(String.Format("Error in copying the post details. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
            }
        }

        private void ClearPostDetail(object data = null)
        {
            try
            {
                Clipboard.Clear();
                app.StatusBarContent = "Clipboard content is cleared successfully.";
            }
            catch (Exception ex)
            {
                app.StatusBarContent = "Error in clearing the post details. Please check error log and contact the support team. ";
                logger.Error(String.Format("Error in clearing the post details. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
            }
        }

        #endregion


    }
}
