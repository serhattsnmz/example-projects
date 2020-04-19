FROM python:3.8-alpine

WORKDIR /app

ADD ./app/requirements.txt ./requirements.txt
RUN pip install -r requirements.txt

ADD ./app ./

EXPOSE 8080

ENTRYPOINT [ "python", "main.py" ]