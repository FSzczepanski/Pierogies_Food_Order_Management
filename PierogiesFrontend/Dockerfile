# build stage
FROM node:lts-alpine as build-stage
ENV TZ=Europe/Warsaw
ENV LANG pl_PL.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}
WORKDIR /app
ADD package*.json ./
RUN npm install --verbose
ADD . .
COPY  ./.env.production ./.env
RUN npm run build -- --verbose

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]