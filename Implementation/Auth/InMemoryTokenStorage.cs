using System.Collections.Concurrent;

namespace API.Core.JWT
{
    public class InMemoryTokenStorage : ITokenStorage
    {
        private static ConcurrentDictionary<Guid, bool> tokens = new ConcurrentDictionary<Guid, bool>();

        public void Add(Guid tokenId)
        {
            int attempts = 0;

            while (attempts < 5)
            {
                var added = tokens.TryAdd(tokenId, true);

                if (added)
                {
                    return;
                }

                attempts++;

                Thread.Sleep(100);
            }

            throw new InvalidOperationException("Token not added to cache.");

        }

        public bool Exists(Guid tokenId)
        {
            if (!tokens.ContainsKey(tokenId))
            {
                return false;
            }

            var isValid = tokens[tokenId];

            return isValid;
        }

        public void Remove(Guid tokenId)
        {
            if (Exists(tokenId))
            {
                var removed = false;
                tokens.Remove(tokenId, out removed);

                if (!removed)
                {
                    throw new InvalidOperationException("Token not removed.");
                }
            }
        }
    }
}
