# Databricks notebook source
configs = {
  "fs.azure.account.auth.type": "CustomAccessToken",
  "fs.azure.account.custom.token.provider.class": spark.conf.get("spark.databricks.passthrough.adls.gen2.tokenProviderClassName")
}

dbutils.fs.mount(
    source = "abfss://dgr-share@dgrpocstorage.dfs.core.windows.net/",
    mount_point = "/mnt/staging",
    extra_configs = configs)

dbutils.fs.mount(
    source = "abfss://dgr-destination@dgrpocstorage.dfs.core.windows.net/",
    mount_point = "/mnt/destination",
    extra_configs = configs)

# COMMAND ----------


