using My_Note.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace My_Note.ViewModels
{
    class NoteViewModel : INotifyPropertyChanged
    {
        //Field
        private string _noteTitle;
        private string _noteContent;
        private Note _selectedNote;

        //get and set
        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NoteContent
        {
            get => _noteContent;
            set
            {
                if (_noteContent != value)
                {
                    _noteContent = value;
                    OnPropertyChanged();
                }
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    NoteTitle = _selectedNote.Title;
                    NoteContent = _selectedNote.Content;
                    OnPropertyChanged();
                }
            }
        }
        

        //Property
        public ObservableCollection<Note> NoteCollection { get; set; }
        public ICommand AddNoteCommand { get; set; }
        public ICommand UpdateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            UpdateNoteCommand = new Command(UpdateNote);
            DeleteNoteCommand = new Command(DeleteNote);

        }

        private void UpdateNote(object obj)
        {
            if(SelectedNote!=null)
            {
                foreach(Note note in NoteCollection.ToList())
                {
                    if (note==SelectedNote)
                    {
                        var newNote = new Note
                        {
                            Id=note.Id,
                            Title= NoteTitle,
                            Content=NoteContent
                        };
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);
                    }
                }
            }
        }

        private void DeleteNote(object obj)
        {
            if(SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
                // rest values
                NoteTitle = string.Empty;
                NoteContent = string.Empty;
            }
        }

        private void AddNote(object obj)
        {
            // generate new ID
            int newId= NoteCollection.Count> 0 ? NoteCollection.Max(n => n.Id) + 1 : 1;
            var note = new Note
            {
                Id = newId,
                Title = _noteTitle,
                Content = _noteContent
            };
            NoteCollection.Add(note);
            // rest values
            NoteTitle = string.Empty;
            NoteContent = string.Empty;
        }


        //Property changed event handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
