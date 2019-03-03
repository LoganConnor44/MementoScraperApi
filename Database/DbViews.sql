CREATE OR REPLACE VIEW MementoScraperDatabase.`MEMENTOS_VW` AS
SELECT MENTO.ID, 
    MENTO.PHRASE,
    MENTO.OWNER,
    MENTO.COMMENT,
    MENTO.TYPE AS SOCIAL_TYPE, 
    MEM.URL,
    CASE
        WHEN UPPER(MEM.MEDIA_TYPE) = 'PHOTO' 
            THEN 'img'
        WHEN UPPER(MEM.MEDIA_TYPE) = 'VIDEO'
            THEN 'video'
        WHEN UPPER(MEM.MEDIA_TYPE) = 'ANIMATED_GIF'
            THEN 'img'
        ELSE MEM.MEDIA_TYPE
    END AS MEDIA_TYPE
FROM MementoScraperDatabase.Mementos MENTO
INNER JOIN MementoScraperDatabase.Memories MEM ON MENTO.ID = MEM.MementoForeignKey;