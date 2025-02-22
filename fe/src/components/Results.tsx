import { IRankingResponse } from "../apis/types/IRankingResponse";

interface ResultsProps {
  results: IRankingResponse[];
  loading: boolean;
}

export default function Results({ results, loading }: ResultsProps) {
  if (loading) {
    return <p className="text-center text-gray-600 mt-4">Fetching results...</p>;
  }
  
  if (results?.length === 0) {
    return <p className="text-center text-gray-600 mt-4">No results found.</p>;
  }

  console.log(results);

  return (
    <div className="mt-6 p-4 bg-gray-100 rounded-lg shadow text-black">
      <h3 className="text-md font-semibold text-gray-600">Results:</h3>
      <ul className="mt-2">
        {results.map((result, index) => (
          <li key={index} className="flex items-center justify-between py-2">
            <span>{result.searchEngine}</span>
            <span>{result.rankings.join(", ")}</span>
          </li>
        ))}
      </ul>
    </div>
  );
}
