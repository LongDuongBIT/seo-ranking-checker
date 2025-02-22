FROM node AS build

WORKDIR /app

COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM node:alpine

WORKDIR /app

COPY --from=build /app/dist /app/build
COPY server.js /app/server.js
RUN npm install -g serve

CMD ["serve", "-s", "build", "-l", "3000"]
