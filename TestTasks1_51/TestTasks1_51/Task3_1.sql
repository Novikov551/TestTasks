CREATE OR REPLACE FUNCTION money_trasaction(
	first_account_id INT,
	second_account_id INT, 
	transaction_summ numeric(19,4)
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
	v_first_balance NUMERIC(19,4);
BEGIN
	SELECT S INTO v_first_balance
	FROM T
	WHERE N = first_account_id;

	IF NOT FOUND THEN RETURN FALSE;
		END IF;

	PERFORM 1 FROM T WHERE N = second_account_id;
	IF NOT FOUND THEN RETURN FALSE;
		END IF;

	IF v_first_balance < transaction_summ THEN
		RETURN FALSE;
	END IF;

	BEGIN
		UPDATE T SET S = (S - transaction_summ) WHERE N = first_account_id;
		UPDATE T SET S = (S + transaction_summ) WHERE N = second_account_id;
		RETURN TRUE;
	EXCEPTION WHEN OTHERS THEN
		RETURN FALSE;
	END;
END;
$$;
