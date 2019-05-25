FROM node:11-alpine

RUN mkdir -p /usr/src/app

RUN apk update

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .