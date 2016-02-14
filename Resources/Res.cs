using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Resources
{
    public static class Res
    {
        public static string SubjectTitle(int id)
        {
            return Common.ResourceManager.GetString(string.Format("Subject{0:D2}", id));
        }

        public static string ArticleTitle(int id)
        {
            return Article.ResourceManager.GetString(string.Format("Article{0:D2}Title", id));
        }

        public static string ArticlePara(int aid, int pid)
        {
            return Article.ResourceManager.GetString(string.Format("Article{0:D2}P{1:D2}", aid, pid));
        }

        public static string SubjectInlineTitle(int subjectId, int titleId)
        {
            return Subject.ResourceManager.GetString(string.Format("Subject{0:D2}Title{1:D2}", subjectId, titleId));
        }

        public static Bitmap GetImage(string resName)
        {
            var bitmap = (Bitmap)ImageRes.ResourceManager.GetObject(resName);
            if (bitmap == null)
            {
                throw new ArgumentException(string.Format("无法找到名为{0}的图片", resName));
            }
            else
            {
                return bitmap;
            }
        }
        //截取正文
        public static string SubText(string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }
            return text.Substring(0, length) + "……";
        }
    }
}
