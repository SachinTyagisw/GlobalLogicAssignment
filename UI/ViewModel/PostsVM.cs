using GlobalLogic.BLL.Controller;
using GlobalLogic.BLL.Model;
using GlobalLogic.Dialog.View;
using GlobalLogic.Dialog.ViewModel;
using GlobalLogic.Helper;
using GlobalLogic.Utilities;
using GlobalLogic_Assignment;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GlobalLogic.UI.ViewModel
{
    class PostsVM : NotifyPropertyChanged
    {
        #region "- Local variables -"
        private DispatcherTimer autoRefreshTimer;
        private App app = Application.Current as App;
        private ILogger logger = new LogService(typeof(PostsVM));
        #endregion

        #region "- Public properties -"

        private Visibility _waitWindowVisibility = Visibility.Collapsed;
        public Visibility WaitWindowVisibility
        {
            get
            {
                return _waitWindowVisibility;
            }
            set
            {
                _waitWindowVisibility = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Post> _posts;
        public ObservableCollection<Post> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private bool _isAutoRefresh;
        public bool IsAutoRefresh
        {
            get
            {
                return _isAutoRefresh;
            }
            set
            {
                _isAutoRefresh = value;

                if ((value == true) && (autoRefreshTimer != null))
                    autoRefreshTimer.Start();
                else
                    autoRefreshTimer.Stop();

                OnPropertyChanged();
            }
        }


        #endregion

        #region "- Constructor -"
        public PostsVM()
        {
            _isAutoRefresh = false;

            autoRefreshTimer = new System.Windows.Threading.DispatcherTimer();
            autoRefreshTimer.Tick += autoRefreshTimer_Tick;
            int intervalInMinute = 0;

            //Default interval for timer shall be set to 10 minute.
            if (!(int.TryParse(ConfigurationManager.AppSettings["autoRefreshTimer"], out intervalInMinute)))
                intervalInMinute = 10;

            autoRefreshTimer.Interval = new TimeSpan(0, intervalInMinute, 0);
            GetPosts();
        }

        void autoRefreshTimer_Tick(object sender, EventArgs e)
        {
            GetPosts();
        }

        #endregion

        #region "- ICommand -"

        private DelegateParameterizedCommand<object> postDetailCommand;
        public ICommand PostDetailCommand
        {
            get
            {
                if (this.postDetailCommand == null)
                {
                    this.postDetailCommand = new DelegateParameterizedCommand<object>(this.PostDetail);
                }
                return this.postDetailCommand;
            }
        }

        private DelegateParameterizedCommand<object> getPostsCommand;
        public ICommand GetPostsCommand
        {
            get
            {
                if (this.getPostsCommand == null)
                {
                    this.getPostsCommand = new DelegateParameterizedCommand<object>(this.GetPosts);
                }
                return this.getPostsCommand;
            }
        }

        #endregion

        #region "- Handler -"

        private void GetPosts(object postId = null)
        {
            app.StatusBarContent = string.Empty;
            ObservableCollection<Post> postCollection = null;
            //get all the data
            if (postId == null)
            {

                using (var workerProcess = new BackgroundWorker())
                {
                    this.WaitWindowVisibility = Visibility.Visible;
                    workerProcess.DoWork += (sender, args) =>
                    {

                        postCollection = new ObservableCollection<Post>(PostController.GetPosts());
                        Users = new ObservableCollection<User>(UserController.GetUsers());
                    };
                    workerProcess.RunWorkerCompleted += (sender, args) =>
                    {
                        try
                        {
                            if (args.Error != null)
                            {
                                if (args.Error is System.Net.WebException)
                                {
                                    app.StatusBarContent = "Problem occured while connecting the website. Please check the network connectivity.";
                                    MessageBox.Show("Problem occured while connecting the website. Please check the network connectivity.");
                                }
                                else
                                {
                                    app.StatusBarContent = "Error in fetching the Post List. Please check error log and contact the support team.";
                                    logger.Error(String.Format("Error in fetching the Post List. Exception:{0} StackTrace:{1}", args.Error.Message, args.Error.StackTrace));
                                }
                            }

                            if (!args.Cancelled && args.Error == null && postCollection != null)
                            {
                                foreach (var post in postCollection)
                                {
                                    post.User = (from user in Users
                                                 where user.ID == post.UserID
                                                 select user).FirstOrDefault();
                                }

                                using (var comparer = new CustomComparer())
                                {
                                    Posts = new ObservableCollection<Post>(postCollection.OrderBy(k => k.ID.ToString(), comparer).ThenBy(k => k.Title, comparer));
                                    app.StatusBarContent = "Records Fetched successfully.";
                                }
                            }

                        }
                        finally
                        {
                            this.WaitWindowVisibility = Visibility.Collapsed;
                            CommandManager.InvalidateRequerySuggested();
                        }
                    };
                    workerProcess.RunWorkerAsync();
                }
            }
        }

        private void PostDetail(object data)
        {
            app.StatusBarContent = string.Empty;
            try
            {
                if (data == null)
                    return;

                var objPost = data as Post;

                if (objPost == null)
                    return;

                var postView = new PostDetailView();
                var dataContext = postView.DataContext as PostDetailVM;
                dataContext.PostData = objPost;
                dataContext.PostDetail();
                
                Window window = new Window
                {
                    Owner = Application.Current.MainWindow,
                    WindowStyle = WindowStyle.ThreeDBorderWindow,
                    Title = "Post Details Window",
                    Content = postView,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                Application.Current.MainWindow.Opacity = 0.5;
                app.StatusBarContent = "Details popup opened successfully.";
                window.Closed += window_Closed;
                window.ShowDialog();
                
            }
            catch (Exception ex)
            {
                app.StatusBarContent = "Error in displaying the details of Post object. Please check error log and contact the support team.";
                logger.Error(String.Format("Error in displaying the post details. Exception:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }

        void window_Closed(object sender, EventArgs e)
        {
            app.StatusBarContent = string.Empty;
            Application.Current.MainWindow.Opacity = 1;
        }

        #endregion

    }
}
