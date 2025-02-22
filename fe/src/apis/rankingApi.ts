import axios from "axios";
import { IRankingRequest } from "./types/IRankingRequest";
import { IRankingResponse } from "./types/IRankingResponse";

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:8080/api";

export async function getSeoRanking(
  params: IRankingRequest
): Promise<IRankingResponse[]> {
  try {
    const response = await axios.post(`${API_BASE_URL}/v1/Ranking`, {
      ...params,
    });

    return response.data;
  } catch (error) {
    console.error("Error fetching SEO ranking:", error);
    throw error;
  }
}