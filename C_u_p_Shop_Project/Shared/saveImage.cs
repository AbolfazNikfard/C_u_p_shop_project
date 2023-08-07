namespace C_u_p_Shop_Project.Shared
{
    public static class saveImages
    {
        public static string createImage(string ImageName, IFormFile Image,string Url)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", Url);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(ImageName)?.ToLower();
            var url = fileName;
            saveImage(Image, Url, fileName);
            return url;
        }
        public static void saveImage(IFormFile upload, string url, string fileName)
        {
            if (upload.Length <= 0) return;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            upload.CopyTo(stream);
        }
    }
}
