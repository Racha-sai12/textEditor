using Microsoft.EntityFrameworkCore;
using TextEditor.Models;

namespace TextEditor.Services
{
    public class EditorService
    {
        private IDbContextFactory<Context> _contextFactory;
        public EditorService(IDbContextFactory<Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateProject(string name)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {
                Project project = new Project
                {
                    Name = name,
                };
                context.Projects.Add(project);
                context.SaveChanges();
            }
        }
        public async Task AddChapter(string title, int projectId)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {
                Chapter chapter = new Chapter
                {
                    Title = title,
                    ProjetId = projectId
                };
                context.Chapters.Add(chapter);
                context.SaveChanges();
            }
        }

        public async Task DeleteProject(int id)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {
               
                Project projectToDelete = context.Projects.Where(x=>x.Id == id).FirstOrDefault();
                if (projectToDelete != null)
                {
                    context.Projects.Remove(projectToDelete);
                    context.SaveChanges();
                }
            }
        }
        public async Task DeleteChapter(int id)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {

                Chapter ChapterToDelete = context.Chapters.Where(x => x.Id == id).FirstOrDefault();
                if (ChapterToDelete != null)
                {
                    context.Chapters.Remove(ChapterToDelete);
                    context.SaveChanges();
                }
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            using (Context context = _contextFactory.CreateDbContext())
            {

                List<Project> projects = context.Projects
                                                .Include(x=>x.Chapters)
                                                .ToList();
                return projects;
            }
        }

        public async Task<string> GetChapterContent(int chapterId)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {
                Chapter Chapter = context.Chapters.Where(x => x.Id == chapterId).FirstOrDefault();
                if (Chapter.Path == null || !File.Exists(Chapter.Path))
                {
                    Chapter.Path = GetChapterLocation(chapterId);
                    using (File.Create(Chapter.Path)) { }
                    return "no content";
                }
                return File.ReadAllText(Chapter.Path);
            }
        }

        public async Task UpdateChapterContent(int chapterId ,string content)
        {
            using (Context context = _contextFactory.CreateDbContext())
            {
                Chapter Chapter = context.Chapters.Where(x => x.Id == chapterId).FirstOrDefault();
                if (Chapter.Path == null || !File.Exists(Chapter.Path))
                {
                    Chapter.Path = GetChapterLocation(chapterId);
                    using (File.Create(Chapter.Path)) { }
                }
                File.WriteAllText(Chapter.Path, content);
                context.Entry(Chapter).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private string GetChapterLocation(int chapterId)
        {
            string folderPath = Path.Combine(FileSystem.AppDataDirectory, "Chapters");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return Path.Combine(folderPath, chapterId.ToString() + ".html");
        }
    }
}
