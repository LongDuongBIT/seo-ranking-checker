export interface IRankingResponse {
  results: RankingResponse[]
}

export interface RankingResponse {
  rankings: number[]
  searchEngine: string
}