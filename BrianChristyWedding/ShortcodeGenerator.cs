using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BrianChristyWedding
{
    public class ShortcodeGenerator
    {
        private readonly string _baseKeyspace;
        private readonly int _length;
        private readonly Random _random = new Random();

        public ShortcodeGenerator(string keyspace, int length)
        {
            if (string.IsNullOrEmpty(keyspace))
                throw new ArgumentException("keyspace");

            if (keyspace.Distinct().Count() != keyspace.Length)
                throw new ArgumentException("keyspace must not contain duplicate characters");

            if (length <= 0)
                throw new ArgumentOutOfRangeException("Shortcode length must be greater than zero");

            _baseKeyspace = keyspace;
            _length = length;
        }

        public string Generate()
        {
            var builder = new StringBuilder();
            var keyspaceSize = _baseKeyspace.Length;

            while (builder.Length < _length)
            {
                var keyIndex = _random.Next(keyspaceSize);
                builder.Append(_baseKeyspace[keyIndex]);
            }
            return builder.ToString();
        }
    }
}