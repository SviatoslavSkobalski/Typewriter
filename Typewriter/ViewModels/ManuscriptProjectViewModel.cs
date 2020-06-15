using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Prism.Regions;
using Typewriter.Core;
using Typewriter.Core.Category;
using Typewriter.Core.Item;
using Typewriter.Core.Project;
using Typewriter.Core.Reference;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.ViewModels
{
    public class ManuscriptProjectViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IManuscriptService _manuscriptService;
        private readonly IWindowsDialogService _windowsDialogService;

        public ManuscriptProjectViewModel(IManuscriptProject manuscriptProject, IRegionManager regionManager, IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
        {
            _regionManager = regionManager;
            _manuscriptService = manuscriptService;
            _windowsDialogService = windowsDialogService;

            Name = manuscriptProject.Name;
            Categories = ToObservableCollection(manuscriptProject.Categories);
            Id = manuscriptProject.Id;

            foreach (var ctg in manuscriptProject.Categories)
            {
                ctg.CollectionChanged += OnProjectCategoryItemsChanged;

                foreach (var item in ctg.Items)
                {
                    item.CollectionChanged += OnProjectItemReferencesChanged;
                }
            }
        }

        public string Name { get; set; }

        public ObservableCollection<ProjectCategoryViewModel> Categories { get; set; }

        public Guid Id { get; }

        public IManuscriptProject ToManuscriptProject(string source)
        {
            return new ManuscriptProject(
                Id,
                Name,
                source,
                ToProjectItemsList(Categories));
        }

        #region Converting to presentation layer
        private ObservableCollection<ProjectCategoryViewModel> ToObservableCollection(IEnumerable<IManuscriptCategory> items)
        {
            var collection = new ObservableCollection<ProjectCategoryViewModel>();

            foreach (var item in items)
            {
                collection.Add(new ProjectCategoryViewModel(item.Name, item.Id, ToObservableCollection(item.Items)));
            }

            return collection;
        }

        private ObservableCollection<ProjectItemViewModel> ToObservableCollection(IEnumerable<IManuscriptItem> items)
        {
            var collection = new ObservableCollection<ProjectItemViewModel>();

            foreach (var item in items)
            {
                collection.Add(new ProjectItemViewModel(item.Name, item.CategoryId, item.Id, item.Source, ToObservableCollection(item.References), _regionManager, _manuscriptService, _windowsDialogService));
            }

            return collection;
        }

        private ObservableCollection<ProjectItemReferenceViewModel> ToObservableCollection(IEnumerable<IManuscriptItemReference> references)
        {
            var collection = new ObservableCollection<ProjectItemReferenceViewModel>();

            foreach (var reference in references)
            {
                collection.Add(new ProjectItemReferenceViewModel(reference.Id, reference.ItemId, reference.Source));
            }

            return collection;
        }
        #endregion

        #region Converting to business object
        private List<IManuscriptItem> ToProjectItemsList(ObservableCollection<ProjectItemViewModel> items)
        {
            var collection = new List<IManuscriptItem>();

            foreach (var item in items)
            {
                collection.Add(new ManuscriptItem(item.Id, item.CtgId, Id, item.Name, item.Source, ToProjectItemsList(item.References)));
            }

            return collection;
        }

        private List<IManuscriptCategory> ToProjectItemsList(ObservableCollection<ProjectCategoryViewModel> categories)
        {
            var collection = new List<IManuscriptCategory>();

            foreach (var category in categories)
            {
                collection.Add(new ManuscriptCategory(Id, category.Id, category.Name, ToProjectItemsList(category.Items)));
            }

            return collection;
        }

        private List<IManuscriptItemReference> ToProjectItemsList(ObservableCollection<ProjectItemReferenceViewModel> references)
        {
            var collection = new List<IManuscriptItemReference>();

            foreach (var reference in references)
            {
                collection.Add(new ManuscriptItemReference(reference.Id, reference.ItemId, reference.Source));
            }

            return collection;
        }
        #endregion

        #region Events Callback
        private void OnProjectCategoryItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!(sender is IManuscriptCategory category))
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    if (!(e.NewItems[0] is IManuscriptItem newItem))
                    {
                        return;
                    }

                    var newItemVm = new ProjectItemViewModel(newItem.Name, newItem.CategoryId, newItem.Id, newItem.Source, ToObservableCollection(newItem.References), _regionManager, _manuscriptService, _windowsDialogService);

                    Categories.Where(c => c.Id == category.Id).FirstOrDefault().Items.Add(newItemVm);

                    newItemVm.IsFirstCreated = true;

                    newItem.CollectionChanged += OnProjectItemReferencesChanged;

                    break;
                case NotifyCollectionChangedAction.Remove:

                    var item = Categories.Where(c => c.Id == category.Id).FirstOrDefault().Items.Where(i => i.Id == e.OldStartingIndex).FirstOrDefault();

                    Categories.Where(c => c.Id == category.Id).FirstOrDefault().Items.Remove(item);

                    break;
                default:
                    break;
            }
        }

        private void OnProjectItemReferencesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!(sender is IManuscriptItem item))
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    if (!(e.NewItems[0] is IManuscriptItemReference newReference))
                    {
                        return;
                    }

                    var collectionItem = Categories.Where(c => c.Id == item.CategoryId).FirstOrDefault().Items.Where(i => i.Id == item.Id).FirstOrDefault();
                    collectionItem.References.Add(new ProjectItemReferenceViewModel(newReference.Id, newReference.ItemId, newReference.Source));
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
