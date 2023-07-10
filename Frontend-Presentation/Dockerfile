FROM node:17-alpine as builder

RUN mkdir -p /usr/src/Frontend-Presentation
WORKDIR /usr/src/Frontend-Presentation
COPY . /usr/src/Frontend-Presentation/

RUN npm install
RUN npm run build --dotenv=.env

FROM node:17-alpine

COPY --from=builder /usr/src/Frontend-Presentation/.output .
COPY --from=builder /usr/src/Frontend-Presentation/.env .

EXPOSE 3000

CMD ["node", "server/index.mjs"]