FROM serhattsnmz/python3.6-env:latest

ADD ./ /app
WORKDIR /app

RUN python -m pip install -r requirements.txt

EXPOSE 8080

ENTRYPOINT python main.py