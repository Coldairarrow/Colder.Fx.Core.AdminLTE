FROM microsoft/dotnet:2.1.3-aspnetcore-runtime-alpine
RUN echo "http://mirrors.aliyun.com/alpine/v3.8/main/" > /etc/apk/repositories \
	&& apk add --no-cache  icu-libs \
	&& apk add --no-cache --repository http://mirrors.aliyun.com/alpine/edge/testing/ libgdiplus
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Web.dll"]