using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiService.Services
{
    public class FileDbService<T> where T: class
    {
        private const string Folder = "Database";
        private readonly string _fileName;

        public FileDbService(string fileName)
        {
            _fileName = fileName;
            Check();
        }

        public async Task<List<T>> GetList()
        {
            var json = await File.ReadAllTextAsync(FullPath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        public async Task<T> Get()
        {
            var json = await File.ReadAllTextAsync(FullPath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task SaveList(List<T> list)
        {
            var json = JsonConvert.SerializeObject(list);
            await File.WriteAllTextAsync(FullPath, json);
        }

        public async Task Save(T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            await File.WriteAllTextAsync(FullPath, json);
        }

        private void Check()
        {
            if (File.Exists(FullPath)) return;
            File.Create(FullPath);
        }

        private string FullPath => Folder + "/" + _fileName;
    }
}
