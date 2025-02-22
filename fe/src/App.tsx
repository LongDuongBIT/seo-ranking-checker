import { useState } from "react";
import { getSeoRanking } from "./apis/rankingApi";
import { IRankingResponse } from "./apis/types/IRankingResponse";
import Results from "./components/Results";
import SearchForm from "./components/SearchForm";

export default function App() {
  const [results, setResults] = useState<IRankingResponse[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSearch = async (keyword: string, url: string, searchEngine: string) => {
    setLoading(true);
    setError("");
    setResults([]);

    try {
      const results = await getSeoRanking({ keyword, url, searchEngines: [searchEngine] });
      setResults(results);
    } catch (err) {
      setError("Failed to fetch search results.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-4 text-black">
      <div className="w-full max-w-lg bg-white p-6 rounded-lg shadow-lg mx-auto">
        <h1 className="text-2xl font-bold text-gray-700 text-center mb-4">
          Search Ranking Checker
        </h1>

        {/* Search Form */}
        <SearchForm onSearch={handleSearch} loading={loading} />

        {/* Error Message */}
        {error && <p className="text-red-500 text-sm text-center mt-3">{error}</p>}

        {/* Display Results */}
        <Results results={results} loading={loading} />
      </div>
    </div>
  );
}
