# Databricks notebook source
from pyspark.sql.functions import from_utc_timestamp, date_format
from pyspark.sql.types import TimestampType
import os

# Making sure storage mounted
dbutils.fs.ls("/mnt/staging")
dbutils.fs.ls("/mnt/destination")

# Reading input params
dbutils.widgets.text("file_path", "", "")
dbutils.widgets.text("portfolio_id", "", "")
dbutils.widgets.text("business_date", "", "")

file_path = dbutils.widgets.get("file_path")
portfolio_id = dbutils.widgets.get("portfolio_id")
business_date = dbutils.widgets.get("business_date")

f'Parameters: file_path = {file_path}, portfolio_id = {portfolio_id}, business_date = {business_date}'

# reading dataframe
src_path = '/mnt/staging/' + file_path
df = spark.read.format('csv').load(src_path)

# saving to parquet
output_path = '/mnt/destination/dgr-data/Portfolio-' + portfolio_id + '.' + business_date + '.parquet'

mode = 'overwrite'
if os.path.exists(output_path):
    mode = 'append'

df.write.format('parquet').mode(mode).save(output_path)

f'Result file: {output_path}'
