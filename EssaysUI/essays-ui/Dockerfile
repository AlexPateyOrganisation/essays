FROM node:24-alpine AS builder
WORKDIR /app
COPY EssaysUI/essays-ui/package.json ./
COPY EssaysUI/essays-ui/package-lock.json ./
RUN --mount=type=cache,target=/root/.npm npm ci
COPY EssaysUI/essays-ui/ ./
RUN npm run build --configuration=development

FROM nginxinc/nginx-unprivileged:alpine AS runner
USER nginx
COPY EssaysUI/essays-ui/nginx.conf /etc/nginx/nginx.conf
COPY --chown=nginx:nginx --from=builder /app/dist/essays-ui/browser /usr/share/nginx/html
EXPOSE 8080
ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]
