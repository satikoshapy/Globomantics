﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Globomantics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Globomantics.Windows.ViewModels
{
    public abstract class BaseTodoViewModel<T> : ObservableObject, ITodoViewMovel
        where T : Todo


    {
        private T? model;
        private string? title;
        private bool isCompleted;
        private Todo? parent;

        public T? Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(IsExisting));
            }
        }
        public string? Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public Todo? Parent
        {
            get => parent;
            set
            {
                parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        }

        public bool IsExisting => Model is not null;

        #region From ITodoViewModel and IViewModel
        public IEnumerable<Todo>? AvailableParentTasks { get ; set ; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; set; } = default!;

        public Action<string>? ShowAlert { get ; set ; }
        public Action<string>? ShowError { get ; set ; }
        public Func<IEnumerable<string>>? ShowOpenFileDialog { get ; set ; }
        public Func<string>? ShowSaveFileDialog { get; set ; }
        public Func<string, bool>? AskForConfirmation { get; set ; }

        #endregion
        public abstract Task SaveAsync();

        public virtual void UpdateModel(Todo model)
        {
            if (model is null)
            {
                return;
            }

            var parent = AvailableParentTasks?.SingleOrDefault(
                t => t.Parent is not null && t.Parent?.Id == model.Parent?.Id);

            Model = model as T;
            Title = model.Title;
            IsCompleted = model.IsCompleted;
            Parent = parent;
        }

        public BaseTodoViewModel()
        {
            DeleteCommand = new RelayCommand(() =>
            {
                if (Model is not null)
                {
                    Model = Model with { IsDeleted = true };
                }
            });
        }
    }
}
