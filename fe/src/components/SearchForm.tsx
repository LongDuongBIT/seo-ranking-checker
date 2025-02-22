import { useState } from "react";

interface SearchFormProps {
  onSearch: (keyword: string, url: string, searchEngine: string) => void;
  loading: boolean;
}

export default function SearchForm({ onSearch, loading }: SearchFormProps) {
  const [keyword, setKeyword] = useState("");
  const [url, setUrl] = useState("");
  const [searchEngine, setSearchEngine] = useState("Google");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!keyword.trim() || !url.trim()) return;
    onSearch(keyword, url, searchEngine);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-6 text-black">
      {/* Keyword Input */}
      <div>
        <label className="block text-sm font-medium text-gray-700">Keyword</label>
        <input
          type="text"
          value={keyword}
          onChange={(e) => setKeyword(e.target.value)}
          className="mt-1 p-3 w-full border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
          placeholder="Enter keyword"
        />
      </div>

      {/* URL Input */}
      <div>
        <label className="block text-sm font-medium text-gray-700">Website URL</label>
        <input
          type="text"
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          className="mt-1 p-3 w-full border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
          placeholder="Enter website URL"
        />
      </div>

      {/* Search Engine Dropdown */}
      <div>
        <label className="block text-sm font-medium text-gray-700">Search Engine</label>
        <select
          value={searchEngine}
          onChange={(e) => setSearchEngine(e.target.value)}
          className="mt-1 p-3 w-full border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
        >
          <option value="Google">Google</option>
          <option value="Bing">Bing</option>
        </select>
      </div>

      {/* Submit Button */}
      <button
        type="submit"
        className="w-full bg-blue-600 text-white p-3 rounded-lg hover:bg-blue-700 transition font-semibold text-lg"
        disabled={loading}
      >
        {loading ? "Searching..." : "Check Rankings"}
      </button>
    </form>
  );
}
