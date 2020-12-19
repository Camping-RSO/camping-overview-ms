CREATE TABLE drzave (
  drzava_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(45) NOT NULL
);

CREATE TABLE regije (
  regija_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(45) NOT NULL,
  drzava INT NOT NULL
);

CREATE TABLE avtokampi (
  avtokamp_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(100) NOT NULL,
  opis VARCHAR(1000) NOT NULL,
  telefon VARCHAR(45) NOT NULL,
  regija INT NOT NULL
);

CREATE TABLE kategorije (
  kategorija_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(45) NOT NULL
);

CREATE TABLE kampirna_mesta (
  kampirno_mesto_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(45) NOT NULL,
  velikost VARCHAR(45) NOT NULL,
  avtokamp INT NOT NULL,
  kategorija INT NOT NULL
);

CREATE TABLE ceniki (
  cenik_id SERIAL PRIMARY KEY NOT NULL,
  naziv VARCHAR(45) NOT NULL,
  cena INT NOT NULL,
  avtokamp INT NOT NULL
);

ALTER TABLE ceniki ADD FOREIGN KEY (avtokamp) REFERENCES avtokampi (avtokamp_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE regije ADD FOREIGN KEY (drzava) REFERENCES drzave (drzava_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE avtokampi ADD FOREIGN KEY (regija) REFERENCES regije (regija_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE kampirna_mesta ADD FOREIGN KEY (avtokamp) REFERENCES avtokampi (avtokamp_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE kampirna_mesta ADD FOREIGN KEY (kategorija) REFERENCES kategorije (kategorija_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX fk_regije_drzave_idx ON regije (drzava);

CREATE INDEX fk_avtokampi_regije_idx ON avtokampi (regija);

CREATE INDEX fk_kampirna_mesta_avtokampi_idx ON kampirna_mesta (avtokamp);

CREATE INDEX fk_kampirna_mesta_kategorije_idx ON kampirna_mesta (kategorija);

CREATE INDEX fk_ceniki_avtokampi_idx ON ceniki (avtokamp);
