// Copyright (c) Red Bull Technology Ltd. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace RaceTrace.Tests.Attributes
{
    /// <summary>
    /// Provides data from a file as an attribute for xunit theory tests.
    /// </summary>
    public class FileDataAttribute : DataAttribute
    {
        private readonly string _filePath;

        /// <summary>
        /// Initialises a new instance of the <see cref="FileDataAttribute"/> class.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        /// <param name="filePath">The file path.</param>
        public FileDataAttribute(string dirPath, string filePath)
        {
            _filePath = $"{dirPath}\\{filePath}";
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));

            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                           ? _filePath
                           : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
                throw new ArgumentException($"Could not find file at path: {path}");

            // Load the file
            var fileData = File.ReadAllText(_filePath);
            object box = fileData;
            return new List<object[]> {new[] {box}};
        }
    }
}