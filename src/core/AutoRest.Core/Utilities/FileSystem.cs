﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutoRest.Core.Utilities
{
    public class FileSystem : IFileSystem
    {
        public void WriteFile(string path, string contents)
        {
            File.WriteAllText(path, contents, new UTF8Encoding(false, true));
        }

        /// <summary>
        /// Returns whether or not that <paramref name="path"/> is an absolute URI or rooted path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsCompletePath(string path)
            => Path.IsPathRooted(path) || Uri.IsWellFormedUriString(path, UriKind.Absolute);

        /// <summary>
        /// Roots the <paramref name="relativePath"/> using the <paramref name="rootPath"/>
        /// Works whether the <paramref name="rootPath"/> is an absolute URI (e.g. https://contoso.com/swaggers)
        /// or a rooted local URI (e.g. C:/swaggers/)
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public string MakePathRooted(Uri rootPath, string relativePath)
        {
            var combined = new Uri(Path.Combine(rootPath.ToString(), relativePath));
            return combined.IsAbsoluteUri ? combined.AbsoluteUri : combined.LocalPath;
        }

        public string ReadFileAsText(string path)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent: AutoRest");
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(path);
            }
        }

        public TextWriter GetTextWriter(string path)
        {
            if (File.Exists(path))
            {
                return File.AppendText(path);
            }
            return File.CreateText(path);
#if FORCE_UTF8_BOM
            // existing ARS files have utf8withbom. 
            // only necessary for some weird manual testing :D
            var utf8WithBom = new System.Text.UTF8Encoding(true);
            return new StreamWriter(path, false, utf8WithBom);
#endif            
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteDirectory(string directory)
        {
            Directory.Delete(directory, true);
        }

        public void EmptyDirectory(string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory))
            {
                File.Delete(filePath);
            }
        }

        public string[] GetFiles(string startDirectory, string filePattern, SearchOption options)
        {
            return Directory.GetFiles(startDirectory, filePattern, options);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string[] GetDirectories(string startDirectory, string filePattern, SearchOption options)
        {
            return Directory.GetDirectories(startDirectory, filePattern, options);
        }

        public string CurrentDirectory => Directory.GetCurrentDirectory();

        public string GetParentDir(string path)
        {
            return Directory.GetParent(Path.Combine(Directory.GetCurrentDirectory(), path)).FullName;
        }
    }
}