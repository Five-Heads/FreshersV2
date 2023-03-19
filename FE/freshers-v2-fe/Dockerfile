### stage 1
##
##FROM node:alpine AS my-app-build
##WORKDIR /app
##COPY . .
##RUN npm ci && npm run build
##
### stage 2
##
##FROM nginx:alpine
##COPY --from=my-app-build /app/dist/app-to-run-inside-docker /usr/share/nginx/html
##EXPOSE 80


##### STAGE 1: Build ###
##FROM node:12.7-alpine AS build
##WORKDIR /usr/src/app
##COPY package.json package-lock.json ./
##RUN npm install
##COPY . .
##RUN npm run build
##
##### STAGE 2: Run ###
##FROM nginx:1.17.1-alpine
##COPY nginx.conf /etc/nginx/nginx.conf
##COPY --from=build /usr/src/app/dist/aston-villa-app /usr/share/nginx/html



# Stage 1
FROM node:18.10 as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod --base-href=./

# Stage 2
FROM nginx:alpine
COPY --from=node /app/dist/freshers-v2-fe /usr/share/nginx/html